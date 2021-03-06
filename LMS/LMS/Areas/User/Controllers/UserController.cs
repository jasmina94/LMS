﻿using LMS.BusinessLogic.AccessControlManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Extension;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.User;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.User.Controllers
{
    public class UserController : Controller
    {
        public IUserService UserService { get; set; }

        public IAccessControlService AccessControlService { get; set; }

        [IsAuthenticated]
        public ActionResult Form(int? id)
        {
            var viewModel = UserService.Get(id);

            return PartialView("UserForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        [IsAuthenticated]
        public JsonResult Save(UserViewModel viewModel)
        {
            var currentUser = Session.GetUser();

            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                SaveUserResult result = UserService.Save(viewModel, currentUser);
                if (result.Success)
                {
                    response.Data = result;
                    if (currentUser.UserId == viewModel.Id)
                    {
                        AccessControlService.SetCurrentUser(Session, currentUser.UserId);
                    }
                }
                else
                {
                    response = Json(result);
                }
            }

            return response;
        }

        [IsAuthenticated]
        public ActionResult Delete(int id)
        {
            UserSessionObject user = Session.GetUser();
            DeleteUserResult result = UserService.Delete(id, user);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About(int id)
        {
            var viewModel = UserService.Get(id);

            return PartialView("About", viewModel);
        }

        [HttpPost]
        public ActionResult CheckUsername(string username)
        {
            var result = UserService.CheckUniqueUsername(username);

            return Json(new { success = result.Item1, id = result.Item2 });
        }

        [HttpPost]
        public ActionResult CheckEmail(string email)
        {
            var result = UserService.CheckUniqueEmail(email);

            return Json(new { success = result.Item1, id = result.Item2 });
        }

        [HttpGet]
        public ActionResult GetSelect(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<UserViewModel> queryLanguageList = UserService.GetAll(true).AsQueryable();
            var languages = new List<UserViewModel>();
            int languagesCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            queryLanguageList = queryLanguageList.Where(x => x.Username.Like(searchTerm) 
                                                          || x.Id.Equals(searchTerm));
            languagesCount = queryLanguageList.Count();

            languages = queryLanguageList.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.UsersToSelectPageResult(languages, languagesCount);

            return new JsonResult
            {
                Data = pagedUsers,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}