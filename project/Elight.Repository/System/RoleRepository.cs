using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class RoleRepository : BaseRepository<Sys_Role>, IRoleRepository
    {
        public Page<Sys_Role> GetList(long pageIndex, long pageSize, string keyWord)
        {
            Sql sql = Sql.Builder
               .Select("r.*, o.FullName")
               .From("Sys_Role r")
               .LeftJoin("Sys_Organize o")
               .On("r.OrganizeId=o.Id")
               .Where("r.DeleteMark=0 and r.Name like @0 or r.EnCode like @1", '%' + keyWord + '%', '%' + keyWord + '%')
               .OrderBy("r.SortCode");

            var list = Db.PageJoin<Sys_Role, Sys_Organize, Sys_Role>((role, dept) =>
            {
                role.DeptName = dept.FullName;
                return role;
            }, pageIndex, pageSize, sql);

            return list;
        }

        public int Delete(params string[] primaryKeys)
        {
            Sql sql = Sql.Builder.Append(" WHERE");
            for (int i = 0; i < primaryKeys.Length - 1; i++)
            {
                sql.Append(" Id=@0 OR", primaryKeys[i]);
            }
            sql.Append(" Id=@0", primaryKeys[primaryKeys.Length - 1]);
            return Db.Delete<Sys_Role>(sql);
        }

        public List<Sys_Role> GetList()
        {
            Sql sql = Sql.Builder.Where("DeleteMark=@0", 0).OrderBy("SortCode");
            return Db.Fetch<Sys_Role>(sql);
        }
    }
}
