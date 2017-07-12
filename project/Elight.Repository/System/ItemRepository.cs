using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class ItemRepository : BaseRepository<Sys_Item>, IItemRepository
    {
        public List<Sys_Item> GetList()
        {
            Sql sql = Sql.Builder.Where("DeleteMark=@0", 0);
            return Db.Fetch<Sys_Item>(sql);
        }

        public Page<Sys_Item> GetList(long pageIndex, long pageSize, string keyWord)
        {
            Sql sql = Sql.Builder.Where("DeleteMark=@0", 0);
            if (!string.IsNullOrEmpty(keyWord))
            {
                sql.Where("Name like @0 or EnCode like @1", '%' + keyWord + '%', '%' + keyWord + '%');
            }
            sql.OrderBy("SortCode");
            return Db.Page<Sys_Item>(pageIndex, pageSize, sql);
        }

        public long GetChildCount(object parentId)
        {
            Sql sql = Sql.Builder.Select("COUNT(*)").From("Sys_Item").Where("ParentId=@0", parentId);
            return Db.ExecuteScalar<long>(sql);
        }
    }
}
