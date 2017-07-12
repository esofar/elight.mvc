using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface IUserRoleRelationRepository : IBaseRepository<Sys_UserRoleRelation>
    {
        /// <summary>
        /// 获取指定用户ID所有用户角色关系实体。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Sys_UserRoleRelation> GetList(string userId);

        /// <summary>
        /// 批量删除用户角色关系实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        int Delete(params string[] userIds);
    }
}
