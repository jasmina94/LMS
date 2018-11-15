using System;
using System.Linq;
using LMS.IR.Indexer;
using LMS.IR.Handler;
using iTextSharp.text.pdf;
using Lucene.Net.Documents;
using LMS.Services.Interfaces;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.Authorization;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Book;
using System.Web;

namespace LMS.BusinessLogic.BookManagement.Implementation
{
    public class EBookServiceImpl : IEBookService
    {
        #region Injected properties

        public IBookRepository BookRepository { get; set; }

        public IBookService BookService { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public DocumentHandler DocumentHandler { get; set; }

        public IEBookIndexer EBookIndexer { get; set; }
        #endregion

        public BookViewModel Get(int? bookId)
        {
            return BookService.Get(bookId);
        }

        public List<BookViewModel> GetAll(bool active)
        {
            var viewModels = BookService.GetAll(active);

            return viewModels.Where(x => x.IsElectronic).ToList();
        }

        public BookViewModel LoadBaseFromFile(string filePath, string fileName)
        {
            var viewModel = new BookViewModel();
            PdfReader reader = new PdfReader(filePath);

            viewModel.UploadSuccess = true;
            viewModel.Author = GetValue(reader, "Author");
            viewModel.Title = GetValue(reader, "Title");
            viewModel.Keywords = GetValue(reader, "Keywords");
            viewModel.Filename = fileName;
            viewModel.MIME = "application/pdf";

            return viewModel;
        }

        public SaveEBookResult SaveAndIndex(EBookCreateViewModel viewModel, string filePath, UserSessionObject user)
        {
            var result = new SaveEBookResult();
            BookViewModel completeViewModel = new BookViewModel(viewModel, user);

            SaveBookResult saveBook = BookService.Save(completeViewModel, user);
            if (saveBook.Success)
            {
                completeViewModel.Id = saveBook.Id;

                if(IndexEBook(completeViewModel, filePath))
                {
                    result = new SaveEBookResult(saveBook.Id, saveBook.Name);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error while indexing e-book.";

                    BookService.Delete(saveBook.Id, user); // rollback transaction
                }
            }
            else
            {
                result.Success = false;
                result.Message = saveBook.Message;
            }

            return result;
        }

        private string GetValue(PdfReader reader, string fieldName)
        {
            string value;

            if (!reader.Info.TryGetValue(fieldName, out value))
            {
                value = string.Empty;
            }

            return value;
        }

        private bool IndexEBook(BookViewModel bookViewModel, string path)
        {
            bool success;
            Document document = null;

            BookDomainModelBuilder builder = BuilderResolverService.Get<BookDomainModelBuilder, BookViewModel>(bookViewModel);
            Constructor.ConstructDomainModelData(builder);
            BookData book = builder.GetDataModel();

            try
            {
                document = DocumentHandler.GetDocument(book, path);
                if(book.Id != 0)
                    EBookIndexer.Delete(book.Id.ToString());

                EBookIndexer.Add(document);
                success = true;
            }
            catch(Exception e)
            {
                success = false;
            }

            return success;
        }

        public bool Delete(int bookId, string path, int userId)
        {
            bool success = false;
            BookData book = BookRepository.GetDataById(bookId);
            if (book != null && DeleteEBookIndex(book, path) && DeleteFile(path))
            {
                BookRepository.DeleteById(bookId, userId);
                success = true;
            }

            return success;
        }

        private bool DeleteEBookIndex(BookData book, string path)
        {
            bool success = false;

            Document document;
            try
            {
                document = DocumentHandler.GetDocument(book, path);
                EBookIndexer.Delete(document);
                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }

        private bool DeleteFile(string filePath)
        {
            bool success = false;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                success = true;
            }

            return success;
        }
    }
}