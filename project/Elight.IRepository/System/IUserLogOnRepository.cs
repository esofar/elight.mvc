using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface IUserLogOnRepository : IBaseRepository<Sys_UserLogOn>
    {
        /// <summary>
        /// 根据用户ID获取用户登陆实体。
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Sys_UserLogOn GetByAccount(string userId);

        /// <summary>
        /// 批量删除用户登陆实体。
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        int Delete(params string[] userIds);

    }
}
