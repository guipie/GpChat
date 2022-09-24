using GuXin.ImCore;
using GuXin.ImCore.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// im 核心类实现的静态代理类
/// </summary>
public static class ImHelper
{
    static ImClient _instance;
    public static ImClient Instance => _instance ?? throw new Exception("使用前请初始化 ImHelper.Initialization(...);");

    /// <summary>
    /// 初始化 ImHelper
    /// </summary>
    /// <param name="options"></param>
    public static void Initialization(ImClientOptions options)
    {
        _instance = new ImClient(options);
    }

    /// <summary>
    /// ImServer 连接前的负载、授权，返回 ws 目标地址，使用该地址连接 websocket 服务端
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="clientMetaData">客户端相关信息，比如ip</param>
    /// <returns>websocket 地址：ws://xxxx/ws?token=xxx</returns>
    public static string PrevConnectServer(Guid clientId, string clientMetaData) => Instance.PrevConnectServer(clientId, clientMetaData);

    /// <summary>
    /// 向指定的多个客户端id发送消息
    /// </summary>
    /// <param name="senderClientId">发送者的客户端id</param>
    /// <param name="receiveClientId">接收者的客户端id</param>
    /// <param name="sendObject">发送对象</param>
    /// <param name="IsReceive">是否回执</param>
    public static bool SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, ChatObject sendObject, bool IsReceive = false) =>
        Instance.SendMessage(senderClientId, receiveClientId, sendObject, IsReceive);

    public static bool SendMessage(int sendUserId, int receiveUserId, ChatObject sendObject, bool IsReceive = false)
    {
        var sendGuid = sendUserId.ToClientGuid();
        var recevieGuid = receiveUserId.ToClientGuid();
        return Instance.SendMessage(sendGuid, new List<Guid> { recevieGuid }, sendObject, IsReceive);
    }

    public static bool SendNotice(int sendUserId, int receiveUserId, string countent, ChatType chatType, bool IsReceive = false)
    {
        var sendGuid = sendUserId.ToClientGuid();
        var recevieGuid = receiveUserId.ToClientGuid();
        return Instance.SendMessage(sendGuid, new List<Guid> { recevieGuid }, new ChatObject()
        {
            Content = countent,
            SendTime = DateTime.Now,
            SendUserId = sendUserId,
            ReceiveUserId = receiveUserId,
            Type = chatType,
            IsGroup = 0
        }, IsReceive);
    }

    public static string[] GetUnreadMessage(Guid userId) => Instance.GetUnreadMessage(userId);

    public static string GetMessage(Guid userId) => Instance.GetMessage(userId);
    public static NoticeObject GetUnreadNotice(int userId, ChatType chatType) => Instance.GetUnreadNotices(userId, chatType);
    /// <summary>
    /// 获取所在线客户端id
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Guid> GetClientListByOnline() => Instance.GetClientListByOnline();

    /// <summary>
    /// 判断客户端是否在线
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public static bool HasOnline(Guid clientId) => Instance.HasOnline(clientId);

    /// <summary>
    /// 事件订阅
    /// </summary>
    /// <param name="online">上线</param>
    /// <param name="offline">下线</param>
    public static void EventBus(
        Action<(Guid clientId, string clientMetaData)> online,
        Action<(Guid clientId, string clientMetaData)> offline) => Instance.EventBus(online, offline);

    #region 群聊频道，每次上线都必须重新加入

    /// <summary>
    /// 加入群聊频道，每次上线都必须重新加入
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="chan">群聊频道名</param>
    public static void JoinChan(Guid clientId, string chan) => Instance.JoinChan(clientId, chan);
    public static void JoinChan(int clientId, string chan) => Instance.JoinChan(clientId.ToClientGuid(), chan);
    /// <summary>
    /// 离开群聊频道
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <param name="chans">群聊频道名</param>
    public static void LeaveChan(Guid clientId, params string[] chans) => Instance.LeaveChan(clientId, chans);
    public static void LeaveChan(int userId, params string[] chans) => Instance.LeaveChan(userId.ToClientGuid(), chans);
    /// <summary>
    /// 获取群聊频道所有客户端id（测试）
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    /// <returns></returns>
    public static Guid[] GetChanClientList(string chan) => Instance.GetChanClientList(chan);
    /// <summary>
    /// 清理群聊频道的离线客户端（测试）
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    public static void ClearChanClient(string chan) => Instance.ClearChanClient(chan);

    /// <summary>
    /// 获取所有群聊频道和在线人数
    /// </summary>
    /// <returns>频道名和在线人数</returns>
    public static IEnumerable<(string chan, long online)> GetChanList() => Instance.GetChanList();
    /// <summary>
    /// 获取用户参与的所有群聊频道
    /// </summary>
    /// <param name="clientId">客户端id</param>
    /// <returns></returns>
    public static string[] GetChanListByClientId(Guid clientId) => Instance.GetChanListByClientId(clientId);
    public static string[] GetChanListByClientId(int clientId) => Instance.GetChanListByClientId(clientId.ToClientGuid());
    /// <summary>
    /// 获取群聊频道的在线人数
    /// </summary>
    /// <param name="chan">群聊频道名</param>
    /// <returns>在线人数</returns>
    public static long GetChanOnline(string chan) => Instance.GetChanOnline(chan);

    /// <summary>
    /// 发送群聊消息，所有在线的用户将收到消息
    /// </summary>
    /// <param name="senderClientId">发送者的客户端id</param>
    /// <param name="chan">群聊频道名</param>
    /// <param name="message">消息</param>
	public static void SendChanMessage(Guid senderClientId, string chan, ChatObject message, bool receive) => Instance.SendChanMessage(senderClientId, chan, message, receive);

    #endregion
}