using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface IItemsDetailRepository : IBaseRepository<Sys_ItemsDetail>
    {
        /// <summary>
        /// 分页获取指定字典选项。
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="itemId">字典ID</param>
        /// <param name="keyWord">选项名称或编码</param>
        /// <returns></returns>
        Page<Sys_ItemsDetail> GetList(long pageIndex, long pageSize, string itemId, string keyWord);

        /// <summary>
        /// 根据字典编号删除所有字典选项。
        /// </summary>
        /// <param name="itemId">字典ID</param>
        /// <returns></returns>
        int Delete(string itemId);
    }
}
