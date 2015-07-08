# NSTool.UMengPush
友盟推送基于2.3sdk封装， 使用C#语法开发的 .NET版 sdk封装，主要封装了android手机推送支持

<font color='red'>注意：使用前请使用Nuget控制台恢复程序集引用。<font>


//例子一，具体参考TEST
<pre>
PostUMengJson postJson = new PostUMengJson();
postJson.type = "broadcast";
postJson.payload = new Payload();
postJson.payload.display_type = "notification";
postJson.payload.body = new ContentBody();
postJson.payload.body.ticker = "评论提醒";
postJson.payload.body.title = "您的评论有回复";
postJson.payload.body.text = "您的评论有回复咯。。。。。";
postJson.payload.body.after_open = "go_custom";
postJson.payload.body.custom = "comment-notify";

postJson.description = "评论提醒-UID:" + 123;

postJson.thirdparty_id = "COMMENT";

ReturnJsonClass resu = umPush.SendMessage(postJson);

//umPush.SendMessage(postJson, callBack);

Assert.AreEqual(resu.ret, "SUCCESS", true);
</pre>
