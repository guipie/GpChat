
#if ns20

using GuXin.ImCore;
using GuXin.ImCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class ImServerExtenssions
{
    static bool isUseWebSockets = false;

    /// <summary>
    /// 启用 ImServer 服务端
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseImServer(this IApplicationBuilder app, ImServerOptions options)
    {
        app.Map(options.PathMatch, appcur =>
        {
            var imserv = new ImServer(options);
            if (isUseWebSockets == false)
            {
                isUseWebSockets = true;
                appcur.UseWebSockets();
            }
            appcur.Use((ctx, next) => imserv.Acceptor(ctx, next));
        });
        return app;
    }
}

/// <summary>
/// im 核心类实现的配置所需
/// </summary>
public class ImServerOptions : ImClientOptions
{
    /// <summary>
    /// 设置服务名称，它应该是 servers 内的一个
    /// </summary>
    public string Server { get; set; }
}

class ImServer : ImClient
{
    protected string _server { get; set; }

    public ImServer(ImServerOptions options) : base(options)
    {
        _server = options.Server;
        _redis.Subscribe($"{_redisPrefix}Server{_server}", RedisSubScribleMessage);
    }

    const int BufferSize = 4096;
    ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ImServerClient>> _clients = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, ImServerClient>>();

    class ImServerClient
    {
        public WebSocket socket;
        public Guid clientId;

        public ImServerClient(WebSocket socket, Guid clientId)
        {
            this.socket = socket;
            this.clientId = clientId;
        }
    }
    internal async Task Acceptor(HttpContext context, Func<Task> next)
    {
        if (!context.WebSockets.IsWebSocketRequest) return;

        string token = context.Request.Query["token"];
        if (string.IsNullOrEmpty(token)) return;
        var token_value = _redis.Get($"{_redisPrefix}Token{token}");

        var socket = await context.WebSockets.AcceptWebSocketAsync();

        if (string.IsNullOrEmpty(token_value))
        {
            //if (context.Request.Headers.ContainsKey("socketGuid"))
            //{
            //    Guid.TryParse(context.Request.Headers.First(m => m.Key == "socketGuid").Value, out Guid currentSocketGuid);
            //    while (socket.State == WebSocketState.Open && _clients.ContainsKey(currentSocketGuid))
            //    { 
            //        socket.Abort();
            //        _clients.TryRemove(currentSocketGuid, out ConcurrentDictionary<Guid, ImServerClient> cd);
            //        Console.WriteLine(context.Request.Headers.First(m => m.Key == "socketGuid").Value + $"下线了.授权错误[{cd.ToJson()}]");
            //    }
            //}
            throw new Exception($"授权错误[{DateTime.Now}]：请先初始化.");
        }
        var data = JsonConvert.DeserializeObject<(Guid clientId, string clientMetaData)>(token_value);
        var cli = new ImServerClient(socket, data.clientId);
        var newid = Guid.NewGuid();

        var wslist = _clients.GetOrAdd(data.clientId, cliid => new ConcurrentDictionary<Guid, ImServerClient>());
        wslist.TryAdd(newid, cli);
        using (var pipe = _redis.StartPipe())
        {
            pipe.HIncrBy($"{_redisPrefix}Online", data.clientId.ToString(), 1);
            pipe.Publish($"evt_{_redisPrefix}Online", token_value);
            pipe.EndPipe();
        }

        var buffer = new byte[BufferSize];
        var seg = new ArraySegment<byte>(buffer);
        try
        {
            while (socket.State == WebSocketState.Open && _clients.ContainsKey(data.clientId))
            {
                var incoming = await socket.ReceiveAsync(seg, CancellationToken.None);
                var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
            }
            socket.Abort();
        }
        catch
        {
        }
        wslist.TryRemove(newid, out var oldcli);
        if (wslist.Any() == false) _clients.TryRemove(data.clientId, out var oldwslist);
        _redis.Eval($"if redis.call('HINCRBY', KEYS[1], '{data.clientId}', '-1') <= 0 then redis.call('HDEL', KEYS[1], '{data.clientId}') end return 1"
                   , new[] { $"{_redisPrefix}Online" });
        _redis.Publish($"evt_{_redisPrefix}Offline", token_value);
    }

    void RedisSubScribleMessage(string chan, object msg)
    {
        try
        {
            var (senderClientId, receiveClientIds, sendObject, receipt) = JsonConvert.DeserializeObject<(Guid senderClientId, Guid[] receiveClientIds, ChatObject sendObject, bool receipt)>(msg as string);
            Console.WriteLine($"收到消息：{sendObject.Serialize()}" + (receipt ? "【需回执】" : "【无需回执】"));
            if (sendObject.Type == ChatType.ChatText || sendObject.Type == ChatType.ChatImage || sendObject.Type == ChatType.ChatVoice || sendObject.Type == ChatType.ChatContent)
                ScribleMessage(senderClientId, receiveClientIds, sendObject, receipt);
            else
                ScribleNotice(senderClientId, receiveClientIds, sendObject);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"订阅方法出错了：{ex.Message}");
        }
    }
    private void ScribleMessage(Guid senderClientId, Guid[] receiveClientIds, ChatObject sendObject, bool receipt)
    {
        foreach (var clientId in receiveClientIds)
        {
            if (clientId == senderClientId && !receipt) continue;
            _redis.LPush(RedisKey.ChatKey(_redisPrefix, clientId), sendObject.Serialize());
            if (_clients.TryGetValue(clientId, out var wslist) == true) //用户在线,则推送告诉有新消息
            {
                ImServerClient[] sockarray = wslist.Values.ToArray();
                foreach (var sh in sockarray)
                {
                    var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(new { sendObject.Type }.ToJson()));
                    sh.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }

    private void ScribleNotice(Guid senderClientId, Guid[] receiveClientIds, ChatObject co)
    {
        try
        {
            foreach (var clientId in receiveClientIds)
            {
                if (clientId == senderClientId) continue;
                var noticeKey = RedisKey.NoticeKey(_redisPrefix, clientId, co.Type);
                _redis.HMSet(noticeKey, "Content", co.Content, "SendTime", co.SendTime);
                _redis.HIncrBy(noticeKey, "Count", 1);
                if (_clients.TryGetValue(clientId, out var wslist)) //用户在线
                {
                    ImServerClient[] sockarray = wslist.Values.ToArray();
                    //如果接收消息人是发送者，并且接收者只有1个以下，则不发送
                    //只有接收者为多端时，才转发消息通知其他端
                    foreach (var sh in sockarray)
                    {
                        var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(new { co.Type }.ToJson()));
                        sh.socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None); //发送客户端通知 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"通知推送出错了：{ex.Message}");
        }
    }


}
#endif