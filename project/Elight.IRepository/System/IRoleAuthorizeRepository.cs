using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface IRoleAuthorizeRepository : IBaseRepository<Sys_RoleAuthorize>
    {
        /// <summary>
        /// 获取所有角色授权信息。
        /// </summary>
        /// <returns></returns>
        List<Sys_RoleAuthorize> GetList();

        /// <summary>
        /// 根据角色ID查询授权信息。
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        List<Sys_RoleAuthorize> GetList(object roleId);

        /// <summary>
        /// 根据权限ID删除角色授权信息。
        /// </summary>
        /// <param name="moduleIds">权限ID集合</param>
        /// <returns></returns>
        int Delete(params string[] moduleIds);

    }
}
