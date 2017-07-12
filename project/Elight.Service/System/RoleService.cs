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
    public partial class RoleService : BaseService<Sys_Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public Page<Sys_Role> GetList(long pageIndex, long pageSize, string keyWord)
        {
            return _roleRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public override object Insert(Sys_Role model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.AllowEdit = model.AllowEdit == null ? false : true;
            model.DeleteMark = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _roleRepository.Insert(model);
        }

        public override int Update(Sys_Role model)
        {
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.AllowEdit = model.AllowEdit == null ? false : true;
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            var updateColumns = new List<string>() { 
                "OrganizeId", "EnCode", "Type", "Name", "AllowEdit",
                "IsEnabled", "Remark", "SortCode", "ModifyUser", "ModifyTime" };
            return _roleRepository.Update(model, updateColumns);
        }

        public int Delete(string[] primaryKeys)
        {
            return _roleRepository.Delete(primaryKeys);
        }

        public List<Sys_Role> GetList()
        {
            return _roleRepository.GetList();
        }
    }
}
