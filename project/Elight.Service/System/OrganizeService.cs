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
    public partial class OrganizeService : BaseService<Sys_Organize>, IOrganizeService
    {
        private readonly IOrganizeRepository _organizeRepository;

        public OrganizeService(IOrganizeRepository organizeRepository)
        {
            this._organizeRepository = organizeRepository;
        }

        public List<Sys_Organize> GetList()
        {
            return _organizeRepository.GetList();
        }

        public Page<Sys_Organize> GetList(long pageIndex, long pageSize, string keyWord)
        {
            return _organizeRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public override object Insert(Sys_Organize model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Layer = _organizeRepository.Get(model.ParentId).Layer += 1;
            model.DeleteMark = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _organizeRepository.Insert(model);
        }

        public override int Update(Sys_Organize model)
        {
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            var updateColumns = new List<string>() {
                "ParentId", "Layer", "EnCode", "FullName", "Type", 
                "ManagerId", "TelePhone", "WeChat", "Fax", "Email" , 
                "Address", "SortCode","IsEnabled","Remark", "ModifyUser" , "ModifyTime"};
            return _organizeRepository.Update(model, updateColumns);
        }

        public long GetChildCount(object parentId)
        {
            return _organizeRepository.GetChildCount(parentId);
        }
    }
}
