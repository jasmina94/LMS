using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.DomainModel.Infrastructure.FilterMapper;
using LMS.DomainModel.Infrastructure.FilterMapper.Model;
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

        public ActionResult SidebarCopy()
        {
            return PartialView();
        }

        public ActionResult Panel()
        {
            return PartialView();
        }

        public ActionResult PanelCopy()
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

        public JsonResult GetAvailableCopies(FilterSorterModel filterSorterModel)
        {
            var bookCopyViewModels = BookService.GetAvailableCopies();
            var filterSorter = new DataCollectionFilterSorter<BookCopyViewModel>();
            IEnumerable<BookCopyViewModel> enumBookCopyValuesViewModel = bookCopyViewModels.AsEnumerable();
            enumBookCopyValuesViewModel = filterSorter.FilterAndSort(enumBookCopyValuesViewModel, filterSorterModel);

            return Json(enumBookCopyValuesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}