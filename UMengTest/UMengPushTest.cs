using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using NSTool.UMengPush;
using NSTool.UMengPush.Core;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace UMengTest
{
    /// <summary>
    /// 仅仅只是演示2个类型推送，其他的都可以自己参考友盟参数的文档进行赋值
    /// </summary>
    [TestClass]
    public class UMengPushTest
    {
        UMengMessagePush<AndroidPostJson> umAndroidPush = new UMengMessagePush<AndroidPostJson>("你的appkey", "你的appMasterSecret");
        UMengMessagePush<IOSPostJson> umIOSPush = new UMengMessagePush<IOSPostJson>("你的appkey", "你的appMasterSecret");
        /// <summary>
        /// 推送给所有用户
        /// </summary>
        [TestMethod]
        public void TestAndroidPushByAllUser()
        {
            AndroidPostJson postJson = new AndroidPostJson();
            postJson.type = CastType.broadcast;
            postJson.payload = new AndroidPayload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = "评论提醒";
            postJson.payload.body.title = "您的评论有回复";
            postJson.payload.body.text = "您的评论有回复咯。。。。。";
            postJson.payload.body.after_open = AfterOpenAction.go_custom;
            postJson.payload.body.custom = "comment-notify";

            postJson.description = "评论提醒-UID:" + 123;

            ReturnJsonClass resu = umAndroidPush.SendMessage(postJson);

            //umPush.SendMessage(postJson, callBack);

            Assert.AreEqual(resu.ret, "SUCCESS", true);
        }

        ///// <summary>
        ///// 根据自定义用户ID推送
        ///// </summary>
        [TestMethod]
        public void TestAndroidPushByAlias()
        {
            AndroidPostJson postJson = new AndroidPostJson();
            postJson.type = CastType.customizedcast;
            postJson.alias_type = "USER_ID";
            postJson.alias = "5583";
            postJson.payload = new AndroidPayload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = "评论提醒Alias";
            postJson.payload.body.title = "您的评论有回复";
            postJson.payload.body.text = "Alias您的评论有回复咯。。。。。";
            postJson.payload.body.after_open = AfterOpenAction.go_custom;
            postJson.payload.body.custom = "comment-notify";

            postJson.description = "评论提醒-UID:" + 5583;

            //ReturnJsonClass resu = umPush.SendMessage(postJson);

            umAndroidPush.AsynSendMessage(postJson, callBack);
        }

        /// <summary>
        /// IOS根据用户token推送
        /// </summary>
        [TestMethod]
        public void TestIOSUniCast()
        {
            IOSPostJson postJson = new IOSPostJson();
            postJson.type = CastType.unicast;
            var aps = new Aps()
            {
                alert = "msg",
                sound = "default"
            };
            var payload = new IOSPayload(aps);
            JObject jo = JObject.FromObject(payload);
            var extra = new Dictionary<string, string>();
            extra.Add("open", "list");
            extra.ToList().ForEach(x => jo.Add(x.Key, x.Value));

            postJson.payload = jo;
            postJson.description = "评论提醒-UID:" + 5583;
            postJson.device_tokens = "your token";
            postJson.production_mode = "false";
            ReturnJsonClass resu = umIOSPush.SendMessage(postJson);
            Assert.AreEqual(resu.ret, "SUCCESS", true);
        }

        private void callBack(ReturnJsonClass result)
        {
            ReturnJsonClass a1 = result;
        }

    }
}
