
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NSTool.UMengPush.Core
{
    public class BasePostJson
    {
        /// <summary>
        /// 必填 应用唯一标识
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 注意：该值由UMengMessagePush自动生成，无需主动赋值
        /// 
        /// 必填 时间戳，10位或者13位均可，时间戳有效期为10分钟 
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 必填 消息发送类型,其值可以为:
        /// <example>
        ///unicast-单播
        ///listcast-列播(要求不超过500个device_token)
        ///filecast-文件播
        ///(多个device_token可通过文件形式批量发送）
        ///broadcast-广播
        ///groupcast-组播
        ///(按照filter条件筛选特定用户群, 具体请参照filter参数)
        ///customizedcast(通过开发者自有的alias进行推送), 
        ///包括以下两种case:
        ///- alias: 对单个或者多个alias进行推送
        ///- file_id: 将alias存放到文件后，根据file_id来推送
        ///</example>
        /// </summary>
        public CastType type { get; set; }
        /// <summary>
        /// 可选 设备唯一表示
        /// 当type=unicast时,必填, 表示指定的单个设备
        /// 当type=listcast时,必填,要求不超过500个,
        /// 多个device_token以英文逗号间隔
        /// </summary>
        public string device_tokens { get; set; }
        /// <summary>
        /// 可选 
        /// 当type=customizedcast时必填，alias的类型, 
        /// alias_type可由开发者自定义,
        /// 开发者在SDK中调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        public string alias_type { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时, 
        /// 开发者填写自己的alias。 要求不超过50个alias,多个alias以英文逗号间隔。
        /// 在SDK中调用setAlias(alias, alias_type)时所设置的alias
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 可选 当type=filecast时，file内容为多条device_token, 
        /// device_token以回车符分隔
        /// 当type = customizedcast时，file内容为多条alias，
        /// alias以回车符分隔，注意同一个文件内的alias所对应
        /// 的alias_type必须和接口参数alias_type一致。
        /// 注意，使用文件播前需要先调用文件上传接口获取file_id, 
        /// 具体请参照"2.4文件上传接口"
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// 可选 终端用户筛选条件,如用户标签、地域、应用版本以及渠道等,
        /// 具体请参考附录G。
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// 是否为生产模式，填写string型true、false
        /// </summary>
        public string production_mode { get; set; }
        /// <summary>
        /// 可选 发送消息描述，建议填写。
        /// </summary>
        public string description { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CastType
    {
        /// <summary>
        /// 单播
        /// </summary>
        unicast,
        /// <summary>
        /// 列播(要求不超过500个device_token)
        /// </summary>
        listcast,
        /// <summary>
        /// 文件播(多个device_token可通过文件形式批量发送）
        /// </summary>
        filecast,
        /// <summary>
        /// 广播
        /// </summary>
        broadcast,
        /// <summary>
        /// 组播(按照filter条件筛选特定用户群, 具体请参照filter参数)
        /// </summary>
        groupcast,
        /// <summary>
        /// (通过开发者自有的alias进行推送)
        /// </summary>
        customizedcast
    }
}
