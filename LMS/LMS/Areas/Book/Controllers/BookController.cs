using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Extension;
using LMS.Models.ViewModels.Book;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public ActionResult GetSelect(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<BookViewModel> queryBookList = BookService.GetAll(true).AsQueryable();
            var books = new List<BookViewModel>();
            int booksCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            queryBookList = queryBookList.Where(x => x.AuthorAndTitle.Like(searchTerm));
            booksCount = queryBookList.Count();

            books = queryBookList.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.BooksToSelectPageResult(books, booksCount);

            return new JsonResult
            {
                Data = pagedUsers,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult Delete(int id)
        {
            DeleteBookResult result = BookService.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}