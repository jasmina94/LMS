using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Models.ViewModels.User;
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
    }
}