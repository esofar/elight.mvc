using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IService
{
    public partial interface IUserService : IBaseService<Sys_User>
    {
        /// <summary>
        /// 根据账号查询用户。
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        Sys_User GetByUserName(string account);

        /// <summary>
        /// 分页获取用户列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">角色编码或名称</param>
        /// <returns></returns>
        Page<Sys_User> GetList(int pageIndex, int pageSize, string keyWord);

        /// <summary>
        /// 批量删除用户。
        /// </summary>
        /// <param name="primaryKeys">主键集合</param>
        /// <returns></returns>
        int Delete(params string[] primaryKeys);

        /// <summary>
        /// 更新用户基础属性信息。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateBasicInfo(Sys_User model);
    }
}
