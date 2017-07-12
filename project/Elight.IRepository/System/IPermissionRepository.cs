using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface IPermissionRepository : IBaseRepository<Sys_Permission>
    {
        /// <summary>
        /// 获取所有权限列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Permission> GetList();

        /// <summary>
        /// 分页获取权限列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">编码或名称</param>
        /// <returns></returns>
        Page<Sys_Permission> GetList(int pageIndex, int pageSize, string keyWord);

        /// <summary>
        /// 批量删除权限。
        /// </summary>
        /// <param name="primaryKeys">权限ID集合</param>
        /// <returns></returns>
        int Delete(params string[] primaryKeys);

        /// <summary>
        /// 获取子级权限数量。
        /// </summary>
        /// <param name="parentId">父级权限ID</param>
        /// <returns></returns>
        long GetChildCount(object parentId);

    }
}
