using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Book;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Models.ViewModels.Book;
using LMS.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class OverviewController : Controller
    {
        public IBookRepository BookRepository { get; set; }

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
            List<BookData> books = BookRepository.GetAllActiveData();
            List<BookViewModel> userViewModels = new List<BookViewModel>();
            DataCollectionFilterSorter<BookViewModel> filterSorter = new DataCollectionFilterSorter<BookViewModel>();

            foreach (var item in books)
            {
                BookViewModelBuilder builder = BuilderResolver.Get<BookViewModelBuilder, BookData>(item);
                Constructor.ConstructViewModelData(builder);
                userViewModels.Add(builder.GetViewModel());
            }

            IEnumerable<BookViewModel> enumBookValuesViewModel = userViewModels.AsEnumerable();
            enumBookValuesViewModel = filterSorter.FilterAndSort(enumBookValuesViewModel, filterSorterModel);

            return Json(enumBookValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}