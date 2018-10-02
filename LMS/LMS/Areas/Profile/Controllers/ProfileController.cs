using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Authorization.Constants;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.Account;
using LMS.Models.ViewModels.User;
using System;
using System.Web.Mvc;

namespace LMS.Areas.Profile.Controllers
{
    public class ProfileController : Controller
    {
        public IUserService UserService { get; set; }

        [IsAuthenticated]
        public ActionResult Index()
        {
            var currentUser = Session.GetUser();
            var viewModel = UserService.Get(currentUser.UserId);

            return View(viewModel);
        }

        public ActionResult ViewChangePassword()
        {
            var viewModel = new PasswordChangeViewModel();

            return PartialView("PasswordChangeForm", viewModel);
        }

        public ActionResult ChangePassword(PasswordChangeViewModel viewModel)
        {
            var currentUser = Session.GetUser();
            UserViewModel userViewModel = UserService.Get(currentUser.UserId);
            SaveUserResult saveResult = null;
            ActionResult result = null;

            if (viewModel.NewPassword == viewModel.RepeatPassword &&
                viewModel.OldPassword != viewModel.NewPassword &&
                viewModel.OldPassword == userViewModel.UserPassword)
            {
                userViewModel.UserPassword = viewModel.NewPassword;
                saveResult = UserService.Save(userViewModel);

                if (saveResult.Success)
                {
                    if (Request.Cookies[SessionConstant.USER_ID] != null)
                    {
                        Response.Cookies[SessionConstant.USER_ID].Expires = DateTime.Now.AddDays(-1);
                    }
                    Session.Remove(SessionConstant.USER);
                    Session.Remove(SessionConstant.USER_ID);
                    Session.Remove(SessionConstant.USERNAME);
                }

                result = Json(saveResult);
            }
            else
            {
                saveResult = new SaveUserResult();
                saveResult.Message = "Data invalid!";
                result = Json(saveResult);
            }

            return result;
        }
    }
}