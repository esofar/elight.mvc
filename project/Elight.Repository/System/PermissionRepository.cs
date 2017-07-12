using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class PermissionRepository : BaseRepository<Sys_Permission>, IPermissionRepository
    {
        public List<Sys_Permission> GetList()
        {
            Sql sql = Sql.Builder.Where("DeleteMark=@0", 0).OrderBy("SortCode");
            return Db.Fetch<Sys_Permission>(sql);
        }
        public Page<Sys_Permission> GetList(int pageIndex, int pageSize, string keyWord)
        {
            Sql sql = Sql.Builder
                .Where("DeleteMark=@0 and Name like @1 or EnCode like @2", 0, '%' + keyWord + '%', '%' + keyWord + '%')
                .OrderBy("SortCode");
            return Db.Page<Sys_Permission>(pageIndex, pageSize, sql);
        }


        public int Delete(params string[] primaryKeys)
        {
            Sql sql = Sql.Builder.Append(" WHERE");
            for (int i = 0; i < primaryKeys.Length - 1; i++)
            {
                sql.Append(" Id=@0 OR", primaryKeys[i]);
            }
            sql.Append(" Id=@0", primaryKeys[primaryKeys.Length - 1]);
            return Db.Delete<Sys_Permission>(sql);
        }

        public long GetChildCount(object parentId)
        {
            Sql sql = Sql.Builder.Select("COUNT(*)").From("Sys_Permission").Where("ParentId=@0", parentId);
            return Db.ExecuteScalar<long>(sql);
        }
    }
}
