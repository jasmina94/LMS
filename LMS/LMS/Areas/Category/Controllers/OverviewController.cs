using LMS.BusinessLogic.CategoryManagement.Interfaces;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.Models.ViewModels.Category;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Category.Controllers
{
    public class OverviewController : Controller
    {
        public ICategoryService CategoryService { get; set; }

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
            var categoryViewModels = CategoryService.GetAll(true);
            var filterSorter = new DataCollectionFilterSorter<CategoryViewModel>();

            IEnumerable<CategoryViewModel> enumCriteriaValuesViewModel = categoryViewModels.AsEnumerable();
            enumCriteriaValuesViewModel = filterSorter.FilterAndSort(enumCriteriaValuesViewModel, filterSorterModel);

            return Json(enumCriteriaValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}