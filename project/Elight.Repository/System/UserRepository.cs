using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elight.Entity;
using Elight.IRepository;

namespace Elight.Repository
{
    public partial class UserRepository : BaseRepository<Sys_User>, IUserRepository
    {

        public Sys_User GetByAccount(string account)
        {
            return base.Db.FirstOrDefault<Sys_User>("where Account=@0", account);
        }

        public Page<Sys_User> GetList(int pageIndex, int pageSize, string keyWord)
        {
            Sql sql = Sql.Builder
                 .Select("u.*, o.FullName")
                 .From("Sys_User u")
                 .LeftJoin("Sys_Organize o")
                 .On("u.DepartmentId=o.Id")
                 .Where("u.DeleteMark=0 and u.Account like @0 or u.RealName like @1", '%' + keyWord + '%', '%' + keyWord + '%')
                 .OrderBy("u.SortCode");

            var list = Db.PageJoin<Sys_User, Sys_Organize, Sys_User>((user, dept) =>
            {
                user.DeptName = dept.FullName;
                return user;
            }, pageIndex, pageSize, sql);

            return list;
        }

        public int Delete(string[] primaryKeys)
        {
            Sql sql = Sql.Builder.Append(" WHERE");
            for (int i = 0; i < primaryKeys.Length - 1; i++)
            {
                sql.Append(" Id=@0 OR", primaryKeys[i]);
            }
            sql.Append(" Id=@0", primaryKeys[primaryKeys.Length - 1]);
            return Db.Delete<Sys_User>(sql);
        }
    }
}
