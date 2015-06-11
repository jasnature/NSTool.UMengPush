using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using NSTool.UMengPush;
using NSTool.UMengPush.Core;

namespace UMengTest
{
    /// <summary>
    /// 仅仅只是演示2个类型推送，其他的都可以自己参考友盟参数的文档进行赋值
    /// </summary>
    [TestClass]
    public class UMengPushTest
    {
        UMengMessagePush umPush = new UMengMessagePush("你的appkey", "你的appMasterSecret");
        
        /// <summary>
        /// 推送给所有用户
        /// </summary>
        [TestMethod]
        public void TestPushByAllUser()
        {
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
        }

        /// <summary>
        /// 根据自定义用户ID推送
        /// </summary>
        [TestMethod]
        public void TestPushByAlias()
        {
            PostUMengJson postJson = new PostUMengJson();
            postJson.type = "customizedcast";
            postJson.alias_type = "USER_ID";
            postJson.alias = "5583";
            postJson.payload = new Payload();
            postJson.payload.display_type = "notification";
            postJson.payload.body = new ContentBody();
            postJson.payload.body.ticker = "评论提醒Alias";
            postJson.payload.body.title = "您的评论有回复";
            postJson.payload.body.text = "Alias您的评论有回复咯。。。。。";
            postJson.payload.body.after_open = "go_custom";
            postJson.payload.body.custom = "comment-notify";

            postJson.thirdparty_id = "COMMENT";

            postJson.description = "评论提醒-UID:" + 5583;

            ReturnJsonClass resu = umPush.SendMessage(postJson);

            //umPush.SendMessage(postJson, callBack);
            Assert.AreEqual(resu.ret, "SUCCESS", true);
        }

        private void callBack(ReturnJsonClass result)
        {
            ReturnJsonClass a1 = result;
        }

    }
}
