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
    public partial class UserRoleRelationService : BaseService<Sys_UserRoleRelation>, IUserRoleRelationService
    {
        private readonly IUserRoleRelationRepository _userRoleRelationRepository;

        public UserRoleRelationService(IUserRoleRelationRepository userRoleRelationRepository)
        {
            this._userRoleRelationRepository = userRoleRelationRepository;
        }

        public List<Sys_UserRoleRelation> GetList(string userId)
        {
            return _userRoleRelationRepository.GetList(userId);
        }

        public void SetRole(string userId, params string[] roleIds)
        {
            //a.用户需要重新设置的角色ID集合。
            var listNewRoleIds = roleIds.ToList();
            //b.用户原有的角色信息。
            var listOldRRs = _userRoleRelationRepository.GetList(userId);
            //c.删除用户新设置和原有用户角色关系集合中相同的记录。
            for (int i = listOldRRs.Count - 1; i >= 0; i--)
            {
                if (listNewRoleIds.Contains(listOldRRs[i].RoleId))
                {
                    listNewRoleIds.Remove(listOldRRs[i].RoleId);
                    listOldRRs.Remove(listOldRRs[i]);
                }
            }
            //d.新集合中剩下的用户角色关系新增到数据库。
            listNewRoleIds.ForEach((roleId) =>
            {
                _userRoleRelationRepository.Insert(new Sys_UserRoleRelation()
                {
                    UserId = userId,
                    RoleId = roleId,
                    Id = Guid.NewGuid().ToString(),
                    CreateUser = OperatorProvider.Instance.Current.Account,
                    CreateTime = DateTime.Now
                });
            });
            //e.旧集合中剩下的用户角色关系从数据库中删除。
            listOldRRs.ForEach((rrObj) =>
            {
                _userRoleRelationRepository.Delete(rrObj);
            });
        }

        public int Delete(params string[] userIds)
        {
            return _userRoleRelationRepository.Delete(userIds);
        }
    }
}
