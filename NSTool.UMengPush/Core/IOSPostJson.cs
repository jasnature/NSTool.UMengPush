using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSTool.UMengPush.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSTool.UMengPush.Core
{
    /// <summary>
    /// 发送到友盟的json实体类
    /// </summary>
    public class IOSPostJson : BasePostJson
    {
        /// <summary>
        /// 必填 消息内容(IOS最大为2012B),
        /// </summary>
        public dynamic payload { get; set; }
        /// <summary>
        /// 可选 发送策略
        /// </summary>
        public IOSPolicy policy { get; set; }
    }


    public class IOSPayload
    {
        public Aps aps { get; set; }
        
        public IOSPayload(Aps _aps)
        {
            this.aps = _aps;
        }
    }

    /// <summary>
    /// 必填 严格按照APNs定义来填写
    /// </summary>    
    public class Aps
    {
        /// <summary>
        /// 必填
        /// </summary>
        public string alert { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string sound { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        public string badge { get; set; }
        /// <summary>
        /// 可选
        /// </summary>
        [JsonProperty(PropertyName = "content-available")]
        public string content_available { get; set; }
        /// <summary>
        /// 可选, 注意: ios8才支持该字段。
        /// </summary>
        public string category { get; set; }
    }

    public class IOSPolicy
    {
        public string start_time { get; set; }
        public string expire_time { get; set; }
        public int max_send_num { get; set; }
        /// <summary>
        /// 可选，iOS10开始生效
        /// </summary>
        [JsonProperty(PropertyName = "apns-collapse-id")]
        public string apns_collapse_id { get; set; }
    }
}
