using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NSTool.UMengPush.Base
{
    /// <summary>
    ///  create by 刘敬
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>    
        /// 从注册表获取文件类型
        /// 你也可以使用ContentTypesConst里面的只读文本，以便加快速度访问
        /// </summary>    
        /// <param name="filename">包含扩展名的文件名</param>    
        /// <returns>文件类型[如：application/stream,]</returns>    
        public static string GetContentType(string filename)
        {
            Microsoft.Win32.RegistryKey fileExtKey = null; ;
            string contentType = "application/stream";
            try
            {
                fileExtKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Path.GetExtension(filename));
                contentType = fileExtKey.GetValue("Content Type", contentType).ToString();
            }
            finally
            {
                if (fileExtKey != null) fileExtKey.Close();
            }
            return contentType;
        }
    }
}
