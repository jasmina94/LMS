using LMS.Models.ViewModels.Account;
using System.Collections.Generic;
using System.Web;

namespace LMS.BusinessLogic.AccessControlManagement.Interfaces
{
    public interface IAccessControlService
    {
        string CheckCookie(HttpRequestBase request);

        void SetCurrentUser(HttpSessionStateBase session, HttpResponseBase response, LoginViewModel loginViewModel);

        void SetCurrentUser(HttpSessionStateBase session, string username);

        bool HasRole(string username, string roleCode);

        bool HasAnyRole(string username, List<string> roleCodes);

        List<string> GetRolesFor(string username);

        List<string> GetPermissionsFor(string username);

        List<string> GetUserPermissions(string username);

        List<string> GetRolePermissions(string username);

        List<string> FilterPermissions(List<string> userPermissions, List<string> rolePermissions);
    }
}
