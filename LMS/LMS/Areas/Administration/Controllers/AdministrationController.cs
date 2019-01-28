using LMS.BusinessLogic.PermissionManagement.Interface;
using LMS.BusinessLogic.PermissionManagement.Model;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.User;
using System.Web.Mvc;

namespace LMS.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        public IPermissionService PermissionService { get; set; }

        [IsAdmin]
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [IsAdmin]
        public ActionResult ViewNewUserPanel()
        {
            return View("NewUser", new UserViewModel());
        }

        [IsAdmin]
        public ActionResult ViewPermissionPanel()
        {
            return View("Permissions");
        }

        [IsAdmin]
        public ActionResult Permissions(int id)
        {
            var data = PermissionService.GetUserPermissions(id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PermissionRemove(PermissionWrapper data)
        {
            var currentUser = Session.GetUser();
            var result = PermissionService.Remove(data.Permissions, data.User, currentUser);

            return Json(data);
        }

        [HttpPost]
        public ActionResult PermissionAssign(PermissionWrapper data)
        {
            var currentUser = Session.GetUser();
            var result = PermissionService.Assign(data.Permissions, data.User, currentUser);

            return Json(data);
        }
    }
}