using LMS.BusinessLogic.LanguageManagement.Interfaces;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.Models.ViewModels.Language;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Language.Controllers
{
    public class OverviewController : Controller
    {
        public ILanguageService LanguageService { get; set; }

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
            var languageViewModels = LanguageService.GetAll(true);
            var filterSorter = new DataCollectionFilterSorter<LanguageViewModel>();

            IEnumerable<LanguageViewModel> enumLanguageValuesViewModel = languageViewModels.AsEnumerable();
            enumLanguageValuesViewModel = filterSorter.FilterAndSort(enumLanguageValuesViewModel, filterSorterModel);

            return Json(enumLanguageValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}