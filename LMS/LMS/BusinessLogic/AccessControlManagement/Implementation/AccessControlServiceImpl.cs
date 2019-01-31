using System;
using System.Collections.Generic;
using System.Web;
using LMS.BusinessLogic.AccessControlManagement.Interfaces;
using LMS.Models.ViewModels.Account;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.DomainModel.Repository.Permission.Interfaces;
using LMS.DomainModel.Repository.Role.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.Infrastructure.Authorization.Constants;
using LMS.Infrastructure.Authorization;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.DomainObject.Relation;

namespace LMS.BusinessLogic.AccessControlManagement.Implementation
{
    public class AccessControlServiceImpl : IAccessControlService
    {
        #region Injected properties

        public IUserRepository UserRepository { get; set; }        

        public IRoleRepository RoleRepository { get; set; }

        public IPermissionRepository PermissionRepository { get; set; }

        public IRelationRolePermissionRepository RelationRolePermissionRepository { get; set; }

        public IRelationUserPermissionRepository RelationUserPermissionRepository { get; set; }

        #endregion

        public int CheckCookie(HttpRequestBase request)
        {
            int userId = 0;

            if (request.Cookies[SessionConstant.USER] != null)
            {
                var id = request.Cookies[SessionConstant.USER_ID].Value;
                userId = int.Parse(id);
            }

            return userId;
        }

        public List<string> FilterPermissions(List<string> userPermissions, List<string> rolePermissions)
        {
            if (userPermissions != null && rolePermissions != null)
            {
                foreach (string permission in userPermissions)
                {
                    if (permission.StartsWith("Deny"))
                    {
                        rolePermissions.Remove(permission.Replace("Deny", ""));
                    }
                    else
                    {
                        if (!rolePermissions.Contains(permission))
                        {
                            rolePermissions.Add(permission);
                        }
                    }
                }
            }
            else
            {
                rolePermissions = new List<string>();
            }

            return rolePermissions;
        }

        public List<string> GetPermissionsFor(string username)
        {
            List<string> userPermissions = GetUserPermissions(username);
            List<string> rolePermissions = GetRolePermissions(username);
            List<string> permissions = FilterPermissions(userPermissions, rolePermissions);

            return permissions;
        }

        public List<string> GetRolePermissions(string username)
        {
            List<string> rolePermissionCodes = new List<string>();
            List<PermissionData> rolePermissions = new List<PermissionData>();

            UserData user = UserRepository.GetUserByUsername(username);         

            var relationRolePermissions = RelationRolePermissionRepository.GetRelationRolePermissionFor(user.RoleId);

            rolePermissionCodes = GetPermissionsFromRolePermissions(relationRolePermissions);

            return rolePermissionCodes;
        }

        private List<string> GetPermissionsFromRolePermissions(List<RelationRolePermissionData> rolePermissions)
        {
            List<string> permissions = new List<string>();
            foreach (RelationRolePermissionData relation in rolePermissions)
            {
                PermissionData permission = PermissionRepository.GetDataById(relation.PermissionId);
                permissions.Add(permission.CodePermission);
            }

            return permissions;
        }

        public List<string> GetRolesFor(string username)
        {
            var roleCodes = new List<string>();

            UserData user = UserRepository.GetUserByUsername(username);

            roleCodes.Add(RoleRepository.GetDataById(user.RoleId).CodeRole);

            return roleCodes;
        }

        public List<string> GetUserPermissions(string username)
        {
            List<string> userPermissionsCodes = new List<string>();
            UserData user = UserRepository.GetUserByUsername(username);
            List<RelationUserPermissionData> relationUserPermissions = RelationUserPermissionRepository.GetRelationUserPermissionFor(user.Id);

            foreach (RelationUserPermissionData relationUserPermission in relationUserPermissions)
            {
                PermissionData permission = PermissionRepository.GetDataById(relationUserPermission.PermissionId);
                userPermissionsCodes.Add(permission.CodePermission);
            }

            return userPermissionsCodes;
        }

        public bool HasAnyRole(string username, List<string> roleCodes)
        {
            bool hasAnyRole = false;

            foreach (string role in roleCodes)
            {
                hasAnyRole = HasRole(username, role);
                if (hasAnyRole)
                    break;
            }

            return hasAnyRole;
        }

        public bool HasRole(string username, string roleCode)
        {
            bool hasRole = false;
            UserData user = UserRepository.GetUserByUsername(username);
            RoleData role = RoleRepository.GetRoleByCode(roleCode);
            
            if(user.RoleId == role.Id)
            {
                hasRole = true;
            }

            return hasRole;
        }

        public void SetCurrentUser(HttpSessionStateBase session, int userId)
        {
            UserSessionObject currentUser = GenerateSessionObjectFor(userId);

            session[SessionConstant.USER] = currentUser;
        }

        public void SetCurrentUser(HttpSessionStateBase session, HttpResponseBase response, LoginViewModel loginViewModel)
        {
            if (session[SessionConstant.USER] == null)
            {
                UserSessionObject currentUser = GenerateSessionObjectFor(loginViewModel.Username);
                session[SessionConstant.USER] = currentUser;

                if (loginViewModel.RememberMe)
                {
                    HttpCookie cookie = new HttpCookie(SessionConstant.USERNAME);
                    cookie.Expires = DateTime.Now.AddSeconds(3600);
                    cookie.Value = loginViewModel.Username;
                    response.Cookies.Add(cookie);
                }
            }
        }

        private UserSessionObject GenerateSessionObjectFor(int userId)
        {
            UserSessionObject currentUser = new UserSessionObject();

            UserData user = UserRepository.GetDataById(userId);
            List<string> roles = GetRolesFor(user.Username);
            List<string> permissions = GetPermissionsFor(user.Username);

            currentUser.Username = user.Username;
            currentUser.Email = user.Email;
            currentUser.UserId = user.Id;
            currentUser.Roles = roles;
            currentUser.Permissions = permissions;
            currentUser.Firstname = user.Firstname;
            currentUser.Lastname = user.Lastname;

            return currentUser;
        }

        private UserSessionObject GenerateSessionObjectFor(string username)
        {
            UserSessionObject currentUser = new UserSessionObject();

            UserData user = UserRepository.GetUserByUsername(username);
            List<string> roles = GetRolesFor(username);
            List<string> permissions = GetPermissionsFor(username);

            currentUser.Username = username;
            currentUser.Email = user.Email;
            currentUser.UserId = user.Id;
            currentUser.Roles = roles;
            currentUser.Permissions = permissions;
            currentUser.Firstname = user.Firstname;
            currentUser.Lastname = user.Lastname;

            return currentUser;
        }
    }
}