using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Extension;
using LMS.Models.ViewModels.User;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Chat.Controllers
{
    public class ChatController : Controller
    {
        public IUserService UserService { get; set; }

        [IsAuthenticated]
        public ActionResult Index()
        {
            return View();
        }

        [IsAuthenticated]
        public ActionResult Group()
        {
            return PartialView();
        }

        [IsAuthenticated]
        public ActionResult AddMembers()
        {
            return PartialView();
        }

        [IsAuthenticated]
        public ActionResult RemoveMember()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetUsers(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<UserViewModel> users = UserService.GetAll(true).AsQueryable();
            List<UserViewModel> listOfUsers = new List<UserViewModel>();
            int usersCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            users = users.Where(user => user.Username.Like(searchTerm));
            usersCount = users.Count();

            listOfUsers = users.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.ChatUsersToSelectPageResult(listOfUsers, usersCount);

            return new JsonResult
            {
                Data = pagedUsers,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}