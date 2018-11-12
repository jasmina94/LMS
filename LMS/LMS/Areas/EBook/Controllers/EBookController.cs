using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.Models.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.EBook.Controllers
{
    public class EBookController : Controller
    {
        public IElectronicBookService EBookService { get; set; }

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

        [HttpPost]
        public ActionResult Save(BookViewModel viewModel)
        {
            return RedirectToAction("Index");
        }
    }
}