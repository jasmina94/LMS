using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Relation;
using LMS.Models.ViewModels.Role;
using System.Collections.Generic;

namespace LMS.BusinessLogic.RoleManagement.Interfaces
{
    public interface IRoleService
    {
        RoleViewModel Get(int? roleId);

        List<RoleViewModel> GetAll(bool active);

        void SaveRelationUserRole(RelationUserRoleViewModel viewModel, UserSessionObject user);
    }
}
