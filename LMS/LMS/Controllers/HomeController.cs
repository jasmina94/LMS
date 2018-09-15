using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.User.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        public IUserRepository UserRepository { get; set; }

        public ActionResult Index()
        {
            List<UserData> users = UserRepository.GetAllActiveData();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}