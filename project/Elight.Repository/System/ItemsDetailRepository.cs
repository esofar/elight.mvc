using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class ItemsDetailRepository : BaseRepository<Sys_ItemsDetail>, IItemsDetailRepository
    {
        public Page<Sys_ItemsDetail> GetList(long pageIndex, long pageSize, string itemId, string keyWord)
        {
            Sql sql = Sql.Builder.Where("DeleteMark=0 and ItemId=@0", itemId);
            if (!string.IsNullOrEmpty(keyWord))
            {
                sql.Where("Name like @0 or EnCode like @1", '%' + keyWord + '%', '%' + keyWord + '%');
            }
            sql.OrderBy("SortCode");
            return Db.Page<Sys_ItemsDetail>(pageIndex, pageSize, sql);
        }

        public int Delete(string itemId)
        {
            Sql sql = Sql.Builder.Where("ItemId=@0", itemId);
            return Db.Delete<Sys_ItemsDetail>(sql);
        }
    }
}
