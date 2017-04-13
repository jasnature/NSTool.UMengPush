using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSTool.UMengPush.Core
{
    public interface IPush<T> where T : BasePostJson
    {
        ReturnJsonClass SendMessage(T paramsJsonObj);

        void AsynSendMessage(T paramsJsonObj, Action<ReturnJsonClass> callback);
    }
}
