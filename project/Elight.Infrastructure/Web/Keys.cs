using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.Infrastructure
{
    /// <summary>
    /// 统一存放Cookie/Session/Cache键。
    /// </summary>
    public partial class Keys
    {
        /// <summary>
        /// 标识验证码。
        /// </summary>
        public const string SESSION_KEY_VCODE = "verify_code";
        /// <summary>
        /// 标识当前登陆用户。
        /// </summary>
        public const string SESSION_KEY_USER = "user_info";
        /// <summary>
        /// 标识用户权限集合。
        /// </summary>
        public const string SESSION_KEY_USER_PER = "user_perssion";
      
    }
}
