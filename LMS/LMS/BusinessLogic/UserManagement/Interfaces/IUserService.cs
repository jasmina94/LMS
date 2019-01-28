using LMS.BusinessLogic.UserManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.User;
using System.Collections.Generic;
using System;

namespace LMS.BusinessLogic.UserManagement.Interfaces
{
    public interface IUserService
    {
        UserViewModel Get(int? userId);

        List<UserViewModel> GetAll(bool active);

        SaveUserResult Save(UserViewModel viewModel, UserSessionObject currentUser);

        DeleteUserResult Delete(int? userId, UserSessionObject currentUser);

        Tuple<bool,int> CheckUniqueUsername(string username);

        Tuple<bool, int> CheckUniqueEmail(string email);
    }
}
