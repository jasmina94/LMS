using LMS.Infrastructure.Authorization.Attributes;
using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.EBook.Controllers
{
    public class EBookController : Controller
    {
        public IEBookService EBookService { get; set; }

        public ActionResult ViewOverview()
        {
            List<BookViewModel> ebooks = EBookService.GetAll(true);

            return View("Overview", ebooks);
        }

        public ActionResult ViewForm(int? id)
        {
            var viewModel = EBookService.Get(id);

            return View("Form", viewModel);
        }

        public ActionResult ViewSearch()
        {
            return View("Search");
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            ActionResult result;
            BookViewModel viewModel = null;
            string filename = string.Empty;

            try
            {
                if (file.ContentLength > 0)
                {
                    filename = string.Format(@"{0}.pdf", Guid.NewGuid());
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    file.SaveAs(path);
                    viewModel = EBookService.LoadBaseFromFile(path, filename);
                }
            }
            catch
            {
                viewModel = new BookViewModel { UploadSuccess = false };
            }

            result = View("Form", viewModel);

            return result;
        }

        [IsAuthenticated]
        public ActionResult Create(EBookCreateViewModel viewModel)
        {
            var relativePath = "~/UploadedFiles/" + viewModel.Filename;
            var absolutePath = HttpContext.Server.MapPath(relativePath);

            SaveEBookResult result = null;
            UserSessionObject user = Session.GetUser();            

            if (System.IO.File.Exists(absolutePath))
            {
                result = EBookService.SaveAndIndex(viewModel, absolutePath, user);
            }
            else
            {
                result = new SaveEBookResult()
                {
                    Success = false,
                    Message = "There is no file with given name. Please repeat upload!"
                };
            }

            return Json(result);
        }

        public ActionResult Delete(int id)
        {
            UserSessionObject user = Session.GetUser();
            BookViewModel book = EBookService.Get(id);

            var relativePath = "~/UploadedFiles/" + book.Filename;
            var absolutePath = HttpContext.Server.MapPath(relativePath);

            bool result = EBookService.Delete(id, absolutePath, user.UserId);
            return Json(new { Success = true });
        }
    }
}