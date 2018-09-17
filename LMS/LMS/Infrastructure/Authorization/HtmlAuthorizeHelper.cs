using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.Authorization
{
    public static class HtmlAuthorizeHelper
    {
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