using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.User.Controllers
{
    public class OverviewController : Controller
    {
        public IUserService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ribbon()
        {
            return PartialView();
        }

        public ActionResult Sidebar()
        {
            return PartialView();
        }

        public ActionResult Panel()
        {
            return PartialView();
        }

        public JsonResult GetAllActive(FilterSorterModel filterSorterModel)
        {
            UserSessionObject currentUser = Session.GetUser();
            List<UserViewModel> userViewModels = UserService.GetAll(true);
            userViewModels.Remove(userViewModels.Single(x => x.Id == currentUser.UserId));

            var filterSorter = new DataCollectionFilterSorter<UserViewModel>();          

            IEnumerable<UserViewModel> enumUserValuesViewModel = userViewModels.AsEnumerable();
            enumUserValuesViewModel = filterSorter.FilterAndSort(enumUserValuesViewModel, filterSorterModel);

            return Json(enumUserValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}