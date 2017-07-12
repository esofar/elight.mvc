using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IService
{
    public partial interface IRoleAuthorizeService : IBaseService<Sys_RoleAuthorize>
    {
        /// <summary>
        /// 根据角色ID查询授权信息。
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        List<Sys_RoleAuthorize> GetList(object roleId);

        /// <summary>
        /// 角色授权。
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="perIds">权限ID集合</param>
        /// <returns></returns>
        void Authorize(string roleId, params string[] perIds);
    }
}
