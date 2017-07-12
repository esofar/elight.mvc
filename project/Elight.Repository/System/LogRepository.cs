using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.IRepository;
using Elight.Entity;

namespace Elight.Repository
{
    public partial class LogRepository : BaseRepository<Sys_Log>, ILogRepository
    {

        public Page<Sys_Log> GetList(long pageIndex, long pageSize, DateTime limitDate, string keyWord)
        {
            Sql sql = Sql.Builder
                .Where("CreateTime > @0", limitDate.ToString())
                .Where("Account like @0 or RealName like @1", '%' + keyWord + '%', '%' + keyWord + '%')
                .OrderBy("CreateTime desc");
            return Db.Page<Sys_Log>(pageIndex, pageSize, sql);
        }

        public int Delete(DateTime keepDate)
        {
            Sql sql = Sql.Builder.Where("CreateTime <= @0", keepDate.ToString());
            return Db.Delete<Sys_Log>(sql);
        }
    }
}
