using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Helpers;
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
    }
}