using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class RoleAuthorizeRepository : BaseRepository<Sys_RoleAuthorize>, IRoleAuthorizeRepository
    {

        public List<Sys_RoleAuthorize> GetList()
        {
            return Db.Fetch<Sys_RoleAuthorize>("");
        }

        public List<Sys_RoleAuthorize> GetList(object roleId)
        {
            Sql sql = Sql.Builder.Where("RoleId=@0", roleId);
            return Db.Fetch<Sys_RoleAuthorize>(sql);
        }

        public int Delete(params string[] moduleIds)
        {
            Sql sql = Sql.Builder.Append(" WHERE");
            for (int i = 0; i < moduleIds.Length - 1; i++)
            {
                sql.Append(" ModuleId=@0 OR", moduleIds[i]);
            }
            sql.Append(" ModuleId=@0", moduleIds[moduleIds.Length - 1]);
            return Db.Delete<Sys_RoleAuthorize>(sql);
        }

    }
}
