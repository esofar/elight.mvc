using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;

namespace Elight.IService
{
    public partial interface IItemService : IBaseService<Sys_Item>
    {
        /// <summary>
        /// 获取所有字典列表。
        /// </summary>
        /// <returns></returns>
        List<Sys_Item> GetList();

        /// <summary>
        /// 分页获取字典列表。
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="keyWord">字典编码或名称</param>
        /// <returns></returns>
        Page<Sys_Item> GetList(long pageIndex, long pageSize, string keyWord);

        /// <summary>
        /// 获取子级字典数量。
        /// </summary>
        /// <param name="parentId">父级字典ID</param>
        /// <returns></returns>
        long GetChildCount(object parentId);
    }
}
