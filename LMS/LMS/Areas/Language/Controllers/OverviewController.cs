using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.DomainModel.Repository.Language.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Language;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Models.ViewModels.Language;
using LMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Language.Controllers
{
    public class OverviewController : Controller
    {
        public ILanguageRepository BookRepository { get; set; }

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

        public JsonResult GetAllActive(FilterSorterModel filterSorterModel)
        {
            List<LanguageData> books = BookRepository.GetAllActiveData();
            List<LanguageViewModel> userViewModels = new List<LanguageViewModel>();
            DataCollectionFilterSorter<LanguageViewModel> filterSorter = new DataCollectionFilterSorter<LanguageViewModel>();

            foreach (var item in books)
            {
                LanguageViewModelBuilder builder = BuilderResolver.Get<LanguageViewModelBuilder, LanguageData>(item);
                Constructor.ConstructViewModelData(builder);
                userViewModels.Add(builder.GetViewModel());
            }

            IEnumerable<LanguageViewModel> enumLanguageValuesViewModel = userViewModels.AsEnumerable();
            enumLanguageValuesViewModel = filterSorter.FilterAndSort(enumLanguageValuesViewModel, filterSorterModel);

            return Json(enumLanguageValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}