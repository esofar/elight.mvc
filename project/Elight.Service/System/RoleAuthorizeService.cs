using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Elight.Infrastructure;
using Elight.Entity;
using Elight.IService;
using Elight.IRepository;

namespace Elight.Service
{
    public partial class RoleAuthorizeService : BaseService<Sys_RoleAuthorize>, IRoleAuthorizeService
    {
        private readonly IRoleAuthorizeRepository _roleAuthorizeRepository;

        public RoleAuthorizeService(IRoleAuthorizeRepository roleAuthorizeRepository)
        {
            this._roleAuthorizeRepository = roleAuthorizeRepository;
        }

        public List<Sys_RoleAuthorize> GetList(object roleId)
        {
            return _roleAuthorizeRepository.GetList(roleId);
        }

        public void Authorize(string roleId, params string[] perIds)
        {
            //a.角色需要重新设置的权限ID集合。
            var listNewPerIds = perIds.ToList();
            //b.角色原有的授权信息。
            var listOldPers = _roleAuthorizeRepository.GetList(roleId);
            //c.删除角色新设置和原有授权信息集合中相同的记录。
            for (int i = listOldPers.Count - 1; i >= 0; i--)
            {
                if (listNewPerIds.Contains(listOldPers[i].ModuleId))
                {
                    listNewPerIds.Remove(listOldPers[i].ModuleId);
                    listOldPers.Remove(listOldPers[i]);
                }
            }
            //d.新集合中剩下的授权信息新增到数据库。
            listNewPerIds.ForEach((perId) =>
            {
                _roleAuthorizeRepository.Insert(new Sys_RoleAuthorize()
                {
                    RoleId = roleId,
                    ModuleId = perId,
                    Id = Guid.NewGuid().ToString(),
                    CreateUser = OperatorProvider.Instance.Current.Account,
                    CreateTime = DateTime.Now
                });
            });
            //e.旧集合中剩下的授权信息从数据库中删除。
            listOldPers.ForEach((perObj) =>
            {
                _roleAuthorizeRepository.Delete(perObj);
            });
        }
    }
}
