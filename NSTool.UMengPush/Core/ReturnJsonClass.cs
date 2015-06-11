using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSTool.UMengPush.Core
{
    public class ReturnJsonClass
    {
        /// <summary>
        /// 返回结果，"SUCCESS"或者"FAIL"
        /// </summary>
        public string ret { get; set; }
        /// <summary>
        /// 结果具体信息
        /// </summary>
        public ResultInfo data { get; set; }
    }

    public class ResultInfo
    {
        /// <summary>
        /// 当"ret"为"SUCCESS"时,包含如下参数:
        /// 当type为unicast、listcast或者customizedcast且alias不为空时:
        /// </summary>
        public string msg_id { get; set; }
        /// <summary>
        /// 当type为于broadcast、groupcast、filecast、customizedcast且file_id不为空的情况(任务)
        /// </summary>
        public string task_id { get; set; }
        /// <summary>
        /// 当"ret"为"FAIL"时,包含如下参数:错误码详见附录I。
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// 如果开发者填写了thirdparty_id, 接口也会返回该值。
        /// </summary>
        public string thirdparty_id { get; set; }
    }
}
