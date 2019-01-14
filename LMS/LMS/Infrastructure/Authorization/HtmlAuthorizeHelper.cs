using LMS.Infrastructure.Authorization.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.Authorization
{
    public static class HtmlAuthorizeHelper
    {
        public static bool HasRole(this HtmlHelper htmlHelper, UserSessionObject currentUser, RoleEnum role)
        {
            bool hasRole = false;
            if(currentUser != null)
            {
                hasRole = currentUser.Roles.Contains(role.ToString());
            }

            return hasRole;
        }

        public static bool HasPermission(this HtmlHelper htmlHelper, UserSessionObject currentUser, string permission)
        {
            bool hasPermission = false;
            if (currentUser != null)
            {
                hasPermission = currentUser.Permissions.Contains(permission);
            }

            return hasPermission;
        }

        public static bool HasPermission(this HtmlHelper htmlHelper, UserSessionObject currentUser, List<string> permissions)
        {
            bool hasPermission = false;
            if (currentUser != null)
            {
                var intersect = currentUser.Permissions.Intersect(permissions);
                if(intersect.Count() != 0)
                {
                    hasPermission = true;
                }
            }
            
            return hasPermission;
        }

        public static IHtmlString Restrict(this HtmlHelper htmlHeper, UserSessionObject currentUser, PermissionEnum permission, IHtmlString content)
        {
            MvcHtmlString mvcHtmlString = null;
            if (currentUser != null && currentUser.Permissions.Contains(permission.ToString()))
                mvcHtmlString = new MvcHtmlString(content.ToString());

            return mvcHtmlString;
        }

        public static IHtmlString Restrict(this HtmlHelper htmlHeper, UserSessionObject currentUser, string neededPermission, IHtmlString content)
        {
            MvcHtmlString mvcHtmlString = null;
            if (currentUser.Permissions.Contains(neededPermission))
                mvcHtmlString = new MvcHtmlString(content.ToString());

            return mvcHtmlString;
        }

        public static IHtmlString Restrict(this HtmlHelper htmlHelper, UserSessionObject currentUser, List<string> neededPermissions, IHtmlString content)
        {
            MvcHtmlString mvcHtmlString = null;
            bool hasAllPermissions = neededPermissions.All(permission => currentUser.Permissions.Contains(permission));
            if (hasAllPermissions)
                mvcHtmlString = new MvcHtmlString(content.ToString());

            return mvcHtmlString;
        }
    }
}