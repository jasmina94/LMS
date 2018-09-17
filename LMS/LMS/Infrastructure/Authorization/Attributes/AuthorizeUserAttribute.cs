using Autofac;
using LMS.Infrastructure.Authorization.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS.Infrastructure.Authorization.Attributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private List<string> permissionsList;

        public List<string> RoleList { get; set; }

        public IComponentContext ComponetContext { get; set; }

        public AuthorizeUserAttribute(string[] roles, params string[] permissions)
        {
            RoleList = roles != null ? roles.ToList() : new List<string>();
            permissionsList = permissions.ToList();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorizeResult = false;
            UserSessionObject currentUser = (UserSessionObject)httpContext.Session[SessionConstant.USER];

            if (currentUser != null)
            {
                if (RoleList.Count != 0 && permissionsList.Count != 0)
                {
                    bool hasRole = CheckRoles(currentUser);
                    bool hasPermission = CheckPermissions(currentUser);
                    authorizeResult = hasRole || hasPermission;
                }
                else if (RoleList.Count == 0 && permissionsList.Count == 0)
                {
                    authorizeResult = true;
                }
                else if (RoleList.Count != 0 && permissionsList.Count == 0)
                {
                    authorizeResult = CheckRoles(currentUser);
                }
                else if (RoleList.Count == 0 && permissionsList.Count != 0)
                {
                    authorizeResult = CheckPermissions(currentUser);
                }
            }
            else
            {
                authorizeResult = false;
            }

            return authorizeResult;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            UserSessionObject currentUser = (UserSessionObject)session[SessionConstant.USER];

            RouteValueDictionary redirectValueDictionary = new RouteValueDictionary();
            redirectValueDictionary.Add("area", "");

            if (currentUser != null)
            {
                redirectValueDictionary.Add("action", "Index");
                redirectValueDictionary.Add("controller", "Home");
            }
            else
            {
                redirectValueDictionary.Add("action", "ShowLoginPanel");
                redirectValueDictionary.Add("controller", "Account");
            }

            filterContext.Result = new RedirectToRouteResult(redirectValueDictionary);
        }

        private bool CheckPermissions(UserSessionObject currentUser)
        {
            bool result = false;
            result = currentUser.Permissions.Any(x => permissionsList.Any(y => y.Equals(x)));
            return result;
        }

        private bool CheckRoles(UserSessionObject currentUser)
        {
            bool result = false;
            result = currentUser.Roles.Any(x => RoleList.Any(y => y.Equals(x)));
            return result;
        }
    }
}