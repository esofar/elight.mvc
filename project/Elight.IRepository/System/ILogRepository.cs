using Elight.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elight.IRepository
{
    public partial interface ILogRepository : IBaseRepository<Sys_Log>
    {
        /// <summary>
        /// 分页获取指定用户操作记录。
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="limitDate">限制日期</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <returns></returns>
        Page<Sys_Log> GetList(long pageIndex, long pageSize, DateTime limitDate, string keyWord);

        /// <summary>
        /// 根据时间删除日志。
        /// </summary>
        /// <param name="keepDate">日志保留时间</param>
        /// <returns></returns>
        int Delete(DateTime keepDate);
    }
}
