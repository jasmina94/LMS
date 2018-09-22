using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Models.ViewModels.Book;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class BookCopyController : Controller
    {
        public IBookService BookService { get; set; }

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
                SaveBookResult result = BookService.Save(viewModel);
                response.Data = result;
            }

            return response;
        }
    }
}