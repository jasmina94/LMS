using System.Web.Mvc;

namespace LMS.Areas.Chat.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}