using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class OrganizeRepository : BaseRepository<Sys_Organize>, IOrganizeRepository
    {
        public List<Sys_Organize> GetList()
        {
            Sql sql = Sql.Builder.Where("DeleteMark=0").OrderBy("SortCode");
            return Db.Fetch<Sys_Organize>(sql);
        }

        public Page<Sys_Organize> GetList(long pageIndex, long pageSize, string keyWord)
        {
            Sql sql = Sql.Builder
               .Where("DeleteMark=@0 and FullName like @1 or EnCode like @2", 0, '%' + keyWord + '%', '%' + keyWord + '%')
               .OrderBy("SortCode");
            return Db.Page<Sys_Organize>(pageIndex, pageSize, sql);
        }

        public long GetChildCount(object parentId)
        {
            Sql sql = Sql.Builder.Select("COUNT(*)").From("Sys_Organize").Where("ParentId=@0", parentId);
            return Db.ExecuteScalar<long>(sql);
        }
    }
}
