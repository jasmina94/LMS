using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Models.ViewModels.Book;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class BookController : Controller
    {
        public IBookService BookService { get; set; }

        public ActionResult Form(int? id)
        {
            var viewModel = BookService.Get(id);

            return PartialView("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        public JsonResult Save(BookViewModel viewModel)
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