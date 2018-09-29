using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Authorization.Constants;
using System.Web;

namespace LMS.Infrastructure.Helpers
{
    public static class SessionHelper
    {
        public static UserSessionObject GetUser(this HttpSessionStateBase session)
        {
            var userSessionObject = (UserSessionObject)session[SessionConstant.USER];

            return userSessionObject;
        }
    }
}