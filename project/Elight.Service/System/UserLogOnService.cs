using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.Infrastructure;
using Elight.IService;
using Elight.IRepository;

namespace Elight.Service
{
    public partial class UserLogOnService : BaseService<Sys_UserLogOn>, IUserLogOnService
    {
        private readonly IUserLogOnRepository _userLogOnRepository;

        public UserLogOnService(IUserLogOnRepository userLogOnRepository)
        {
            this._userLogOnRepository = userLogOnRepository;
        }

        public override object Insert(Sys_UserLogOn model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.SecretKey = model.Id.DESEncrypt().Substring(0, 8);
            model.Password = model.Password.DESEncrypt(model.SecretKey).MD5Encrypt();
            model.LoginCount = 0;
            model.IsOnLine = false;
            return _userLogOnRepository.Insert(model);
        }

        public Sys_UserLogOn GetByAccount(string userId)
        {
            return _userLogOnRepository.GetByAccount(userId);
        }

        public int UpdateInfo(Sys_UserLogOn model)
        {
            var updateColumns = new List<string>() {
                 "AllowMultiUserOnline", "Question", "AnswerQuestion",  "CheckIPAddress", "Language", "Theme"};
            return _userLogOnRepository.Update(model, updateColumns);
        }

        public int UpdateLogin(Sys_UserLogOn model)
        {
            model.IsOnLine = true;
            model.LastVisitTime = DateTime.Now;
            model.PrevVisitTime = model.LastVisitTime;
            model.LoginCount += 1;
            var updateColumns = new List<string>() { 
                "IsOnLine", "PrevVisitTime", "LastVisitTime", "LoginCount" };
            return _userLogOnRepository.Update(model, updateColumns);
        }

        public int Delete(params string[] userIds)
        {
            return _userLogOnRepository.Delete(userIds);
        }

        public bool ModifyPwd(Sys_UserLogOn model)
        {
            model.ChangePwdTime = DateTime.Now;
            var updateColumns = new List<string>() { "Password", "ChangePwdTime" };
            return _userLogOnRepository.Update(model, updateColumns) > 0 ? true : false;
        }
    }
}
