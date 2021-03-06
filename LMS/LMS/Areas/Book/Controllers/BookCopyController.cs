﻿using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.Book;
using LMS.Models.ViewModels.Relation;
using System.Web.Mvc;

namespace LMS.Areas.Book.Controllers
{
    public class BookCopyController : Controller
    {
        public IBookService BookService { get; set; }

        public ILoanService LoanService { get; set; }

        [IsAuthenticated]
        public ActionResult Form(int id)
        {
            var viewModel = new BookCopyViewModel();
            var referencingBook = BookService.Get(id);
            
            viewModel.BookId = id;
            viewModel.BookAuthorAndTitle = referencingBook.AuthorAndTitle;

            return PartialView("BookCopyForm", viewModel);
        }

        [IsAuthenticated]
        public ActionResult FormComplex()
        {
            var viewModel = new BookCopyViewModel();

            return PartialView("BookCopyFormComplex", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        [IsAuthenticated]
        public ActionResult Save(BookCopyViewModel viewModel)
        {
            UserSessionObject user = Session.GetUser();
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;

            if (validation.Success)
            {
                viewModel.Id = 0;
                viewModel.Available = true;

                SaveBookResult result = BookService.Save(viewModel, user);
                response.Data = result;
            }

            return response;
        }

        [IsAuthenticated]
        public ActionResult Loan(int id)
        {
            var viewModel = LoanService.CreateLoanModel(id);

            return PartialView("LoanForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        [IsAuthenticated]
        public ActionResult Borrow(RelationUserBookCopyViewModel viewModel)
        {
            UserSessionObject user = Session.GetUser();
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                viewModel.Id = 0;
                BorrowResult result = LoanService.BorrowBook(viewModel, user);
                response.Data = result;
            }

            return response;
        }

        [IsAuthenticated]
        public ActionResult Restore(int id)
        {
            var result = LoanService.ReturnBook(id, Session.GetUser());

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [IsAuthenticated]
        public ActionResult Delete(int id)
        {
            UserSessionObject user = Session.GetUser();
            DeleteBookResult result = BookService.DeleteCopy(id, user);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}