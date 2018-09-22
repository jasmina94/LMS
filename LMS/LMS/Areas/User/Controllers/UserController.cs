using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Extension;
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

        public ActionResult Form(int? id)
        {
            var viewModel = UserService.Get(id);

            return PartialView("UserForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        public JsonResult Save(UserViewModel viewModel)
        {
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                SaveUserResult result = UserService.Save(viewModel);
                response.Data = result;
            }

            return response;
        }

        public ActionResult About(int id)
        {
            var viewModel = UserService.Get(id);

            return PartialView("About", viewModel);
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