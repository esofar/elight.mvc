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
    public partial class ItemService : BaseService<Sys_Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        public List<Sys_Item> GetList()
        {
            return _itemRepository.GetList();
        }

        public Page<Sys_Item> GetList(long pageIndex, long pageSize, string keyWord)
        {
            return _itemRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public long GetChildCount(object parentId)
        {
            return _itemRepository.GetChildCount(parentId);
        }

        public override object Insert(Sys_Item model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Layer = _itemRepository.Get(model.ParentId).Layer += 1;
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.DeleteMark = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _itemRepository.Insert(model);
        }

        public override int Update(Sys_Item model)
        {
            model.Layer = _itemRepository.Get(model.ParentId).Layer += 1;
            model.IsEnabled = model.IsEnabled == null ? false : true;
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            var updateColumns = new List<string>() {
                "ParentId", "Layer", "EnCode", "Name", "SortCode", 
                "IsEnabled",  "Remark","ModifyUser", "ModifyTime" };
            return _itemRepository.Update(model, updateColumns);
        }
    }
}
