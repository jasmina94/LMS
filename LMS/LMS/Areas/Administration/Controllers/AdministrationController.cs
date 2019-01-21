using LMS.Infrastructure.Authorization.Attributes;
using LMS.Models.ViewModels.User;
using System.Web.Mvc;

namespace LMS.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        [IsAdmin]
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        [IsAdmin]
        public ActionResult ViewPermissionPanel()
        {
            return PartialView("Permissions");
        }
    }
}