using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.Entity
{
    public partial class Sys_User
    {

        /// <summary>
        /// 保存角色部门名称。
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 保存用户角色ID。
        /// </summary>
        public List<string> RoleId { get; set; }

    }
}
