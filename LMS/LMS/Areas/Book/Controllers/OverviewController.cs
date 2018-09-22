using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using LMS.Infrastructure.ModelBuilders.Implementation.Book;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class OverviewController : Controller
    {
        public IBookService BookService { get; set; }

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
            var bookViewModels = BookService.GetAll(true);
            var filterSorter = new DataCollectionFilterSorter<BookViewModel>();
            IEnumerable<BookViewModel> enumBookValuesViewModel = bookViewModels.AsEnumerable();
            enumBookValuesViewModel = filterSorter.FilterAndSort(enumBookValuesViewModel, filterSorterModel);

            return Json(enumBookValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}