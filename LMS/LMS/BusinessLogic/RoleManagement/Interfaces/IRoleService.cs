using LMS.Models.ViewModels.Role;
using System.Collections.Generic;

namespace LMS.BusinessLogic.RoleManagement.Interfaces
{
    public interface IRoleService
    {
        RoleViewModel Get(int? roleId);

        List<RoleViewModel> GetAll(bool active);

        RoleViewModel GetRoleFor(int userId);
    }
}
