using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using NSTool.UMengPush.Base;
using NSTool.UMengPush.Core;
using Newtonsoft.Json;

namespace NSTool.UMengPush
{
    /// <summary>
    /// 友盟消息推送
    /// create by jasnature
    /// </summary>
    public class UMengMessagePush<T> : IPush<T>, IDisposable where T : BasePostJson
    {
        #region 内部字段

        private RestClient requestClient;

        private MD5CryptionUMeng md5;

        private Encoding encoder = Encoding.UTF8;

        private string requestProtocol = "http";

        private string requestMethod = "POST";

        private string hostUrl = "msg.umeng.com";

        private string postPath = "api/send";

        private string apiFullUrl = null;

        private string appkey = null;

        private string appMasterSecret = null;

        protected const string USER_AGENT = "Push-Server/4.5";

        #endregion

        #region 公共方法

        /// <summary>
        /// 使用默认的参数构造,参数从友盟网站的应用信息中获取
        /// </summary>
        /// <param name="appkey">appkey</param>
        /// <param name="appMasterSecret">App Master Secret，供API对接友盟服务器使用的密钥</param>
        public UMengMessagePush(string appkey, string appMasterSecret)
        {
            this.apiFullUrl = string.Concat(requestProtocol, "://", hostUrl, "/", postPath, "/");
            this.appkey = appkey;
            this.appMasterSecret = appMasterSecret;
            this.md5 = new MD5CryptionUMeng();
        }

        /// <summary>
        /// 推送消息，注意如果初始化本类已经填写了appkey，
        /// <paramref name="paramsJsonObj"/> 里面的appkey会自动赋值
        /// 反之如果您填写了<paramref name="paramsJsonObj"/> 里面的appkey，将采用参数里面的值，忽略本类初始化值。
        /// </summary>
        /// <param name="paramsJsonObj"></param>
        /// <returns></returns>
        public ReturnJsonClass SendMessage(T paramsJsonObj)
        {
            var request = CreateHttpRequest(paramsJsonObj);
            var resultResponse = requestClient.Execute(request);
            ReturnJsonClass rjs = SimpleJson.DeserializeObject<ReturnJsonClass>(resultResponse.Content);
            return rjs;
        }

        /// <summary>
        /// 异步推送消息，注意如果初始化本类已经填写了appkey，
        /// <paramref name="paramsJsonObj"/> 里面的appkey会自动赋值
        /// 反之如果您填写了<paramref name="paramsJsonObj"/> 里面的appkey，将采用参数里面的值，忽略本类初始化值。 
        /// </summary>
        /// <param name="paramsJsonObj"></param>
        /// <param name="callback"></param>
        public void AsynSendMessage(T paramsJsonObj, Action<ReturnJsonClass> callback)
        {
            var request = CreateHttpRequest(paramsJsonObj);

            requestClient.ExecuteAsync(request, resultResponse =>
            {
                callback?.Invoke(SimpleJson.DeserializeObject<ReturnJsonClass>(resultResponse.Content));
            });
        }

        #endregion

        #region 私有辅助方法

        private RestRequest CreateHttpRequest(T paramsJsonObj)
        {
            string bodyJson = InitParamsAndUrl(paramsJsonObj);

            if (requestClient == null)
            {
                requestClient = new RestClient(apiFullUrl);
                requestClient.Encoding = Encoding.UTF8;
                requestClient.UserAgent = USER_AGENT;
            }
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
            return request;
        }

        private string InitParamsAndUrl(T paramsJsonObj)
        {
            if (string.IsNullOrEmpty(paramsJsonObj.appkey)) paramsJsonObj.appkey = this.appkey;
            //重置url
            this.apiFullUrl = string.Concat(requestProtocol, "://", hostUrl, "/", postPath, "/");

            paramsJsonObj.timestamp = GetTimeStamp().ToString();

            JsonSerializerSettings jssetting = new JsonSerializerSettings();
            jssetting.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(paramsJsonObj, jssetting);

            string calcSign = md5.GenerateMD5(requestMethod + apiFullUrl + json + appMasterSecret).ToLower();

            this.apiFullUrl = string.Format("{0}?sign={1}", this.apiFullUrl, calcSign);

            return json;
        }

        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private uint GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToUInt32(ts.TotalSeconds);
        }        

        #endregion

        public void Dispose()
        {
            if (md5 != null) md5.Dispose();
        }
    }
}
