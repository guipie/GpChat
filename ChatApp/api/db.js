// 打开数据库  

// 执行SQL语句
export function executeSQL(sql, success, error) {
	let options = {
		name: getApp().globalData.userId,
		path: '_doc/guxin_db' + getApp().globalData.userId + '1.db'
	}
	if (!plus.sqlite.isOpenDatabase(options))
		plus.sqlite.openDatabase(options);
	plus.sqlite.executeSql({
		name: options.name,
		sql: sql,
		success: function(data) {
			console.log("sql执行成功", data, sql);
			if (success) success(data);
		},
		fail: function(e) {
			console.log('executeSql failed: ' + JSON.stringify(e), sql);
			if (error) error(e);
		}
	});
}
// 查询SQL语句
export function selectSQL(sql, successFun, errorFun) {
	let options = {
		name: getApp().globalData.userId,
		path: '_doc/guxin_db' + getApp().globalData.userId + '1.db'
	}
	if (!plus.sqlite.isOpenDatabase(options))
		plus.sqlite.openDatabase(options);
	plus.sqlite.selectSql({
		name: options.name,
		sql: sql,
		success: function(data) {
			console.log('selectSql success: ', sql, data);
			if (successFun) successFun(data);
		},
		fail: function(e) {
			console.log('selectSql failed: ' + JSON.stringify(e), sql);
			if (errorFun) errorFun(e);
		}
	});
}
//初始化，创建app所需的表
export function dbInit() {
	let tables = new Array();
	tables[0] = 'CREATE TABLE if not exists chat(' +
		'Id integer primary key autoincrement NOT NULL,' +
		'SendUserId   INT(11) NOT NULL,' +
		'ReceiveUserId    INT(11) NOT NULL,' + //如果是群消息此字段为群ID
		'Content          NVARCHAR(2000) NOT NULL,' +
		'Type         INYINT default(1),' + //消息类别 1文字。2图片 3语音
		'FileInfo  NVARCHAR(2000),' +
		'NoticeType         INYINT default(0),' + //消息通知类别，默认消息。0消息，1系统通知，2分享好友，3分享内容。
		'IsGroup   INYINT default(0) NOT NULL,' +
		'SendTime     TIMESTAMP default (datetime(\'now\', \'localtime\')),' +
		'ReceiveTime     TIMESTAMP default (datetime(\'now\', \'localtime\')));' //所有聊天消息表   
	tables[1] = 'CREATE TABLE if not exists last_chat(' +
		'Id integer primary key autoincrement NOT NULL,' +
		'[FriendUserId]   int(11) NOT NULL,' +
		'[Name]   NVARCHAR(20)  NOT NULL,' +
		'[Avatar] NVARCHAR(2000)  NOT NULL,' +
		'Content  NVARCHAR(2000) NOT NULL,' +
		'UnReadCount     INYINT default(0) NOT NULL,' +
		'Type     INYINT default(1) NOT NULL,' +
		'IsGroup   INYINT default(0) NOT NULL,' +
		'[Top] SMALLINT NOT NULL,' +
		'[NotDisturb] SMALLINT NOT NULL,' +
		'SendTime   TIMESTAMP  default (datetime(\'now\', \'localtime\')) );' //最后一条聊天记录
	tables[2] = 'CREATE TABLE if not exists friend(' +
		'[FriendUserId]   int(11) primary key NOT NULL,' +
		'[FriendRemarkName]  NVARCHAR(20)  NOT NULL,' +
		'[Avatar]  NVARCHAR(200)  NULL,' +
		'[Status] SMALLINT NOT NULL,' +
		'[Top] SMALLINT NOT NULL,' +
		'[NotDisturb] SMALLINT NOT NULL,' +
		'[BackgroundImage] NVARCHAR(200),' +
		'[CreateDate] datetime NOT NULL,' +
		'[ModifyDate] datetime NOT NULL);' //好友列表
	tables[3] = 'CREATE TABLE if not exists chat_group(' +
		'[GroupId]   int(11) primary key NOT NULL,' +
		'[Name]  NVARCHAR(20)  NOT NULL,' +
		'[Avatar]  NVARCHAR(2000)  NULL,' +
		'[Top] SMALLINT NOT NULL,' +
		'[SaveGroup] SMALLINT NOT NULL,' +
		'[AcceptAdd] SMALLINT NOT NULL,' +
		'[NotDisturb] SMALLINT NOT NULL,' +
		'[BackgroundImage] NVARCHAR(200),' +
		'[Description] NVARCHAR(200),' +
		'[CreateID] int(11) NOT NULL,' +
		'[CreateDate] datetime NOT NULL,' +
		'[ModifyDate] datetime NOT NULL);' //群列表
	tables[4] = 'CREATE TABLE if not exists chat_group_member(' +
		'Id integer primary key autoincrement NOT NULL,' +
		'[GroupId]   int(11) NOT NULL,' +
		'[UserId]   int(11)  NOT NULL,' +
		'[NickName]  NVARCHAR(20)  NOT NULL,' +
		'[Avatar]  NVARCHAR(200)  NULL,' +
		'[CreateDate] datetime NOT NULL,' +
		'[ModifyDate] datetime);' //群成员
	executeSQL(tables, () => {
		console.log("本地数据库初始化成功.")
	}, (e) => {
		console.log('本地数据库初始化成功失败: ' + JSON.stringify(e));
	});
}
