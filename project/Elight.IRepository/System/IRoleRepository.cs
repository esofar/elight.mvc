using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;

namespace Elight.IRepository
{
    public partial interface IRoleRepository : IBaseRepository<Sys_Role>
    {
        /// <summary>
        /// 获取所有角色列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Role> GetList();
        Page<Sys_Role> GetList(long pageIndex, long pageSize, string keyWord);
        int Delete(params string[] primaryKeys);
    }
}
