using LMS.Infrastructure.Authorization.Constants;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS.Infrastructure.Authorization.Attributes
{
    public class IsAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            UserSessionObject currentUser = (UserSessionObject)session[SessionConstant.USER];

            if (currentUser == null)
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