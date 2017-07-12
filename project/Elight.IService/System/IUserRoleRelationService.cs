using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IService
{
    public partial interface IUserRoleRelationService : IBaseService<Sys_UserRoleRelation>
    {
        /// <summary>
        /// 根据用户ID获取角色集合。
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<Sys_UserRoleRelation> GetList(string userId);

        /// <summary>
        /// 给用户配置角色信息。   
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID集合</param>
        void SetRole(string userId, params string[] roleIds);

        /// <summary>
        /// 批量删除用户角色关系实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        int Delete(params string[] userIds);

    }
}
