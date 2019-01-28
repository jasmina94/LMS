using System.Collections.Generic;
using LMS.Models.ViewModels.Permission;
using LMS.Infrastructure.Authorization;
using LMS.BusinessLogic.PermissionManagement.Model;

namespace LMS.BusinessLogic.PermissionManagement.Interface
{
    public interface IPermissionService
    {
        PermissionViewModel Get(int? permissionId);

        List<PermissionViewModel> GetAll(bool active);

        AdministrationUserPermissionViewModel GetUserPermissions(int userId);

        PermissionResult Assign(List<int> permissions, int userId, UserSessionObject currentUser);

        PermissionResult Remove(List<int> permissions, int userId, UserSessionObject currentUser);
    }
}
