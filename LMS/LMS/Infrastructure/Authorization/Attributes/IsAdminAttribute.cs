using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LMS.Infrastructure.Authorization.Constants;

namespace LMS.Infrastructure.Authorization.Attributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            UserSessionObject currentUser = (UserSessionObject)session[SessionConstant.USER];

            if (!currentUser.Roles.Contains(RoleEnum.Admin.ToString()))
            {
                RouteValueDictionary redirectValueDictionary = new RouteValueDictionary();
                redirectValueDictionary.Add("action", "ShowNotAllowed");
                redirectValueDictionary.Add("controller", "Home");
                redirectValueDictionary.Add("area", "");
                filterContext.Result = new RedirectToRouteResult(redirectValueDictionary);
            }

            base.OnActionExecuting(filterContext);
        }

    }
}