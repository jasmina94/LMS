using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Models.ViewModels.Book;
using LMS.Models.ViewModels.Relation;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class BookCopyController : Controller
    {
        public IBookService BookService { get; set; }

        public ILoanService LoanService { get; set; }

        public ActionResult Form(int id)
        {
            var viewModel = new BookCopyViewModel();
            var referencingBook = BookService.Get(id);
            
            viewModel.BookId = id;
            viewModel.BookAuthorAndTitle = referencingBook.AuthorAndTitle;

            return PartialView("BookCopyForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        public ActionResult Save(BookCopyViewModel viewModel)
        {
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                viewModel.Id = 0;
                SaveBookResult result = BookService.Save(viewModel);
                response.Data = result;
            }

            return response;
        }

        public ActionResult Loan(int id)
        {
            var viewModel = LoanService.CreateLoanModel(id);

            return PartialView("LoanForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        public ActionResult Borrow(RelationUserBookCopyViewModel viewModel)
        {
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                viewModel.Id = 0;
                BorrowResult result = LoanService.BorrowBook(viewModel);
                response.Data = result;
            }

            return response;
        }

        public ActionResult Delete(int id)
        {
            DeleteBookResult result = BookService.DeleteCopy(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}