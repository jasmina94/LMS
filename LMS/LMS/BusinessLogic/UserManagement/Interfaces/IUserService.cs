using LMS.BusinessLogic.UserManagement.Model;
using LMS.Models.ViewModels.User;
using System.Collections.Generic;

namespace LMS.BusinessLogic.UserManagement.Interfaces
{
    public interface IUserService
    {
        UserViewModel Get(int? userId);

        List<UserViewModel> GetAll(bool active);

        SaveUserResult Save(UserViewModel viewModel);

        DeleteUserResult Delete(int? userId);
    }
}
