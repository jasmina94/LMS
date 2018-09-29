using System.Web.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InfoOne()
        {
            return View();
        }

        public ActionResult InfoTwo()
        {
            return View();
        }

        public ActionResult InfoThree()
        {
            return View();
        }

        public ActionResult ShowNotAllowed()
        {
            return PartialView("NotAllowed");
        }
    }
}