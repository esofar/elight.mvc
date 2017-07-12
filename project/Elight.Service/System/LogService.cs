using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Infrastructure;
using Elight.Entity;
using Elight.IService;
using Elight.IRepository;

namespace Elight.Service
{
    public partial class LogService : BaseService<Sys_Log>, ILogService
    {

        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository _logRepository)
        {
            this._logRepository = _logRepository;
        }

        public Page<Sys_Log> GetList(long pageIndex, long pageSize, DateTime limitDate, string keyWord)
        {
            return _logRepository.GetList(pageIndex, pageSize, limitDate, keyWord);
        }

        public int Delete(DateTime keepDate)
        {
            return _logRepository.Delete(keepDate);
        }
    }
}
