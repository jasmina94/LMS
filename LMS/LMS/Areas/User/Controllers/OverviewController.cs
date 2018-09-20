using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.User;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Models.ViewModels.User;
using LMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Users.Controllers
{
    public class OverviewController : Controller
    {
        public IUserRepository UserRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolver { get; set; }

        // GET: Users/Overview
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Overview/Ribbon
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

        [HttpPost]
        public JsonResult GetAllActive(FilterSorterModel filterSorterModel)
        {
            List<UserData> users = UserRepository.GetAllActiveData();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            DataCollectionFilterSorter<UserViewModel> filterSorter = new DataCollectionFilterSorter<UserViewModel>();

            foreach (var item in users)
            {
                UserViewModelBuilder builder = BuilderResolver.Get<UserViewModelBuilder, UserData>(item);
                Constructor.ConstructViewModelData(builder);
                userViewModels.Add(builder.GetViewModel());
            }

            IEnumerable<UserViewModel> enumUserValuesViewModel = userViewModels.AsEnumerable();
            enumUserValuesViewModel = filterSorter.FilterAndSort(enumUserValuesViewModel, filterSorterModel);

            return Json(enumUserValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}