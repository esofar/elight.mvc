using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class UserRoleRelationRepository : BaseRepository<Sys_UserRoleRelation>, IUserRoleRelationRepository
    {
        public List<Sys_UserRoleRelation> GetList(string userId)
        {
            return Db.Fetch<Sys_UserRoleRelation>("where UserId=@0", userId);
        }
        public int Delete(params string[] userIds)
        {
            Sql sql = Sql.Builder.Append(" WHERE");
            for (int i = 0; i < userIds.Length - 1; i++)
            {
                sql.Append(" UserId=@0 OR", userIds[i]);
            }
            sql.Append(" UserId=@0", userIds[userIds.Length - 1]);
            return Db.Delete<Sys_UserRoleRelation>(sql);
        }
    }
}
