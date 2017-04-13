using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSTool.UMengPush.Base;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace NSTool.UMengPush.Core
{
    /// <summary>
    /// 发送到友盟的安卓json实体类
    /// </summary>
    public class AndroidPostJson : BasePostJson
    {
        /// <summary>
        /// 必填 消息内容(Android最大为1840B),
        /// </summary>
        public AndroidPayload payload { get; set; }
        /// <summary>
        /// 可选 发送策略
        /// </summary>
        public AndroidPolicy policy { get; set; }
    }


    public class AndroidPayload
    {
        /// <summary>
        /// 必填 消息类型，值可以为:notification-通知，message-消息
        /// </summary>
        public string display_type { get; set; }
        /// <summary>
        /// 必填 消息体
        /// </summary>
        public ContentBody body { get; set; }

        /// <summary>
        /// 可选 用户自定义key-value。只对"通知"(display_type=notification)生效。
        /// 可以配合通知到达后,打开App,打开URL,打开Activity使用。
        /// </summary>
        public SerializableDictionary<string, string> extra { get; set; }
    }

    public class ContentBody
    {
        /// <summary>
        /// 必填 通知栏提示文字
        /// </summary>
        public string ticker { get; set; }
        /// <summary>
        /// 必填 通知标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 必填 通知文字描述
        /// </summary>
        public string text { get; set; }
        public string icon { get; set; }
        public string largeIcon { get; set; }
        public string img { get; set; }
        public string sound { get; set; }
        public int builder_id { get; set; }
        public string play_vibrate { get; set; }
        public string play_lights { get; set; }
        public string play_sound { get; set; }
        public AfterOpenAction after_open { get; set; }
        public string url { get; set; }
        public string activity { get; set; }
        public string custom { get; set; }
    }

    public class AndroidPolicy
    {
        public string start_time { get; set; }
        public string expire_time { get; set; }
        public int max_send_num { get; set; }
        public string out_biz_no { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AfterOpenAction
    {
        /// <summary>
        /// 打开应用
        /// </summary>
        go_app,
        /// <summary>
        /// 跳转到URL
        /// </summary>
        go_url,
        /// <summary>
        /// 打开特定的activity
        /// </summary>
        go_activity,
        /// <summary>
        /// 用户自定义内容。
        /// </summary>
        go_custom
    }
}
