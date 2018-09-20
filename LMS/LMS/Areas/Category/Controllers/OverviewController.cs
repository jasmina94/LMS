using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Category;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Models.ViewModels.Category;
using LMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Category.Controllers
{
    public class OverviewController : Controller
    {
        public ICategoryRepository CategoryRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolver { get; set; }

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

        [HttpPost]
        public JsonResult GetAllActive(FilterSorterModel filterSorterModel)
        {
            List<CategoryData> categories = CategoryRepository.GetAllActiveData();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            DataCollectionFilterSorter<CategoryViewModel> filterSorter = new DataCollectionFilterSorter<CategoryViewModel>();

            foreach (var item in categories)
            {
                CategoryViewModelBuilder builder = BuilderResolver.Get<CategoryViewModelBuilder, CategoryData>(item);
                Constructor.ConstructViewModelData(builder);
                categoryViewModels.Add(builder.GetViewModel());
            }

            IEnumerable<CategoryViewModel> enumCriteriaValuesViewModel = categoryViewModels.AsEnumerable();
            enumCriteriaValuesViewModel = filterSorter.FilterAndSort(enumCriteriaValuesViewModel, filterSorterModel);

            return Json(enumCriteriaValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}