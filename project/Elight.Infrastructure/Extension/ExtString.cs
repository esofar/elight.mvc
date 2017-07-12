using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Elight.Infrastructure
{
    /// <summary>
    /// 字符串对象扩展方法。
    /// </summary>
    public static class ExtString
    {

        /// <summary>
        /// 判断字符串是否为空。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 字符串分割成字符串数组。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符，默认为逗号。</param>
        /// <returns></returns>
        public static string[] ToStrArray(this string str, string separator = ",")
        {
            return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
