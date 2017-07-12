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
    public partial class ItemsDetailService : BaseService<Sys_ItemsDetail>, IItemsDetailService
    {
        private readonly IItemsDetailRepository _itemsDetailRepository;

        public ItemsDetailService(IItemsDetailRepository itemsDetailRepository)
        {
            this._itemsDetailRepository = itemsDetailRepository;
        }

        public Page<Sys_ItemsDetail> GetList(long pageIndex, long pageSize, string itemId, string keyWord)
        {
            return _itemsDetailRepository.GetList(pageIndex, pageSize, itemId, keyWord);
        }

        public int Delete(string itemId)
        {
            return _itemsDetailRepository.Delete(itemId);
        }

        public override object Insert(Sys_ItemsDetail model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.IsDefault = model.IsDefault == null ? false : true;
            model.DeleteMark = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _itemsDetailRepository.Insert(model);
        }

        public override int Update(Sys_ItemsDetail model)
        {
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.IsDefault = model.IsDefault == null ? false : true;
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            var updateColumns = new List<string>() {
                "ItemId", "EnCode", "Name", "IsDefault", "SortCode", 
                "IsEnabled",  "ModifyUser","ModifyTime" };
            return _itemsDetailRepository.Update(model, updateColumns);
        }
    }
}
