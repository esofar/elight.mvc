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
    public partial class PermissionService : BaseService<Sys_Permission>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleAuthorizeRepository _roleAuthorizeRepository;
        private readonly IUserRoleRelationRepository _userRoleRelationRepository;

        public PermissionService(IPermissionRepository permissionRepository, IRoleAuthorizeRepository roleAuthorizeRepository,
            IUserRoleRelationRepository userRoleRelationRepository)
        {
            this._permissionRepository = permissionRepository;
            this._roleAuthorizeRepository = roleAuthorizeRepository;
            this._userRoleRelationRepository = userRoleRelationRepository;
        }

        public override object Insert(Sys_Permission model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Layer = _permissionRepository.Get(model.ParentId).Layer += 1;
            model.IsEnable = model.IsEnable == null ? false : true;
            model.IsEdit = model.IsEdit == null ? false : true;
            model.IsPublic = model.IsPublic == null ? false : true;
            model.DeleteMark = false;
            model.CreateUser = OperatorProvider.Instance.Current.Account;
            model.CreateTime = DateTime.Now;
            model.ModifyUser = model.CreateUser;
            model.ModifyTime = model.CreateTime;
            return _permissionRepository.Insert(model);
        }

        public override int Update(Sys_Permission model)
        {
            model.Layer = _permissionRepository.Get(model.ParentId).Layer += 1;
            model.IsEnable = model.IsEnable == null ? false : true;
            model.IsEdit = model.IsEdit == null ? false : true;
            model.IsPublic = model.IsPublic == null ? false : true;
            model.ModifyUser = OperatorProvider.Instance.Current.Account;
            model.ModifyTime = DateTime.Now;
            var updateColumns = new List<string>() {
                "ParentId", "Layer", "EnCode", "Name", "JsEvent", "Icon", 
                "Url", "Remark", "Type", "SortCode", "IsPublic", "IsEnable", 
                "IsEdit", "ModifyUser", "ModifyTime" };
            return _permissionRepository.Update(model, updateColumns);
        }

        public List<Sys_Permission> GetList()
        {
            return _permissionRepository.GetList();
        }

        public List<Sys_Permission> GetList(string userId)
        {
            //a.根据用户ID查询角色ID集合 （一对多关系）
            var listRoleIds = _userRoleRelationRepository.GetList(userId).Select(c => c.RoleId).ToList();
            //b.根据角色ID查询权限ID集合 （多对多关系）
            var listModuleIds = _roleAuthorizeRepository.GetList().Where(c => listRoleIds.Contains(c.RoleId)).Select(c => c.ModuleId).ToList();
            //c.根据权限ID集合查询所有权限实体。
            return _permissionRepository.GetList().Where(c => listModuleIds.Contains(c.Id) && c.IsEnable == true).ToList();
        }

        public Page<Sys_Permission> GetList(int pageIndex, int pageSize, string keyWord)
        {
            return _permissionRepository.GetList(pageIndex, pageSize, keyWord);
        }

        public bool ActionValidate(string userId, string action)
        {
            var authorizeModules = new List<Sys_Permission>();
            authorizeModules = WebHelper.GetCache<List<Sys_Permission>>("authorize_modules_" + userId);
            if (authorizeModules == null)
            {
                authorizeModules = this.GetList(userId);
                //设置缓存有效时间20分钟。
                WebHelper.SetCache<List<Sys_Permission>>("authorize_modules_" + userId, authorizeModules, DateTime.Now.AddMinutes(20));
            }
            foreach (var item in authorizeModules)
            {
                if (!string.IsNullOrEmpty(item.Url))
                {
                    string[] url = item.Url.Split('?');
                    if (url[0].ToLower() == action.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int Delete(params string[] primaryKeys)
        {
            //删除权限与角色的对应关系。
            _roleAuthorizeRepository.Delete(primaryKeys);
            return _permissionRepository.Delete(primaryKeys);
        }

        public long GetChildCount(object parentId)
        {
            return _permissionRepository.GetChildCount(parentId);
        }
    }
}
