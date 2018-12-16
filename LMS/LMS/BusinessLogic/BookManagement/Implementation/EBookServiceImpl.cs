using System.Linq;
using LMS.IR.Model;
using LMS.IR.Indexer;
using LMS.IR.Handler;
using iTextSharp.text.pdf;
using Lucene.Net.Documents;
using LMS.Services.Interfaces;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using LMS.Models.ViewModels.Search;
using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.Authorization;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Book;
using LMS.IR.QueryLogic;
using Lucene.Net.Search;
using Autofac;
using System.Web.Hosting;
using LMS.IR.LanguageAnalysis;
using System;
using LMS.BusinessLogic.LanguageManagement.Interfaces;
using LMS.IR.Retriever;
using LMS.BusinessLogic.CategoryManagement.Interfaces;

namespace LMS.BusinessLogic.BookManagement.Implementation
{
    public class EBookServiceImpl : IEBookService, IStartable
    {
        #region Injected properties

        public IBookRepository BookRepository { get; set; }

        public IBookService BookService { get; set; }

        public ILanguageService LanguageService { get; set; }

        public ICategoryService CategoryService { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public DocumentHandler DocumentHandler { get; set; }

        public IEBookIndexer EBookIndexer { get; set; }

        private readonly string RAW_DIR_PATH = HostingEnvironment.MapPath(@"~//UploadedFiles");
        private readonly string INDEX_DIR_PATH = HostingEnvironment.MapPath(@"~/Index");

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

            string language = LanguageService.Get(bookViewModel.LanguageId).Name;
            IndexerType type = AnalyzerService.GetIndexerType(language);

            BookDomainModelBuilder builder = BuilderResolverService.Get<BookDomainModelBuilder, BookViewModel>(bookViewModel);
            Constructor.ConstructDomainModelData(builder);
            BookData book = builder.GetDataModel();
            
            try
            {
                document = DocumentHandler.GetDocument(book, path);
                if (book.Id != 0)
                    EBookIndexer.DeleteById(book.Id.ToString(), type);

                EBookIndexer.Add(document, type);
                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }

        public bool Delete(int bookId, string path, int userId)
        {
            bool success = false;
            BookData book = BookRepository.GetDataById(bookId);
            string language = LanguageService.Get(book.LanguageId).Name;

            if (book != null && DeleteEBookIndex(book, path, language) && DeleteFile(path))
            {
                BookRepository.DeleteById(bookId, userId);
                success = true;
            }

            return success;
        }

        private bool DeleteEBookIndex(BookData book, string path, string language)
        {
            bool success = false;
            Document document;
            IndexerType type = AnalyzerService.GetIndexerType(language);
            try
            {
                document = DocumentHandler.GetDocument(book, path);
                EBookIndexer.DeleteByDocument(document, type);
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

        public List<ResultData> Search(SingleFieldSearchViewModel sfsViewModel)
        {
            var results = new List<ResultData>();
            string fieldName = sfsViewModel.FieldName.Trim();
            string fieldValue = sfsViewModel.FieldValue.Trim();

            QueryType queryType = (QueryType)Enum.Parse(typeof(QueryType), sfsViewModel.QueryType);
            IndexerType type = AnalyzerService.GetIndexerType(sfsViewModel.Language);

            try
            {
                Query query = QueryBuilder.BuildQuery(type, queryType, fieldName, fieldValue);
                InformationRetriever informationRetriever = new InformationRetriever(type, RAW_DIR_PATH, INDEX_DIR_PATH);

                var queriedHighlights = new List<string>() { fieldName };

                results = informationRetriever.RetrieveEBooks(type, query, queriedHighlights, Sort.INDEXORDER);

                if(results.Count != 0)
                {
                    MapLanguageAndCategory(results);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }

        public List<ResultData> Search(MultiFieldSearchViewModel mfsViewModel)
        {
            var results = new List<ResultData>();
            var requiredHighlights = new List<string>();
            var booleanQuery = new BooleanQuery();
            IndexerType type = AnalyzerService.GetIndexerType(mfsViewModel.Language);

            InformationRetriever informationRetriever = new InformationRetriever(type, RAW_DIR_PATH, INDEX_DIR_PATH);

            QueryType queryType = (QueryType)Enum.Parse(typeof(QueryType), mfsViewModel.QueryType);
            QueryOperator queryOperator = (QueryOperator)Enum.Parse(typeof(QueryOperator), mfsViewModel.QueryOperator);
            Occur occur = queryOperator.Equals(QueryOperator.AND)
                ? Occur.MUST
                : Occur.SHOULD;

            try
            {
                if (!string.IsNullOrEmpty(mfsViewModel.Title))
                {
                    requiredHighlights.Add("Title");
                    booleanQuery.Add(QueryBuilder.BuildQuery(type, queryType, "Title", mfsViewModel.Content.Trim()), occur);
                }

                if (!string.IsNullOrEmpty(mfsViewModel.Author))
                {
                    requiredHighlights.Add("Author");
                    booleanQuery.Add(QueryBuilder.BuildQuery(type, queryType, "Author", mfsViewModel.Content.Trim()), occur);
                }

                if (!string.IsNullOrEmpty(mfsViewModel.Keywords))
                {
                    requiredHighlights.Add("Keyword");
                    List<Query> queries = BuildQueriesForKeywords(type, queryType, occur, mfsViewModel.Keywords);
                    queries.ForEach(x => booleanQuery.Add(x, occur));
                }

                if (!string.IsNullOrEmpty(mfsViewModel.Content))
                {
                    requiredHighlights.Add("Content");
                    booleanQuery.Add(QueryBuilder.BuildQuery(type, queryType, "Content", mfsViewModel.Content.Trim()), occur);
                }

                if (!string.IsNullOrEmpty(mfsViewModel.Language))
                {
                    requiredHighlights.Add("Language");
                    booleanQuery.Add(QueryBuilder.BuildQuery(type, queryType, "Language", mfsViewModel.Language.Trim()), occur);
                }

                results = informationRetriever.RetrieveEBooks(type, booleanQuery, requiredHighlights, Sort.INDEXORDER);
            }
            catch (Exception e)
            {
                results = null;
            }

            return results;
        }

        private List<Query> BuildQueriesForKeywords(IndexerType indexerType, QueryType queryType, Occur occur, string keywords)
        {
            var queries = new List<Query>();
            if (keywords.Contains(","))
            {
                string[] parts = keywords.Split(',');
                foreach (string part in parts)
                {
                    if (!string.IsNullOrEmpty(part))
                    {
                        try
                        {
                            Query query = QueryBuilder.BuildQuery(indexerType, queryType, "Keyword", part.Trim());
                            queries.Add(query);
                        }
                        catch (Exception e)
                        {
                            queries = new List<Query>();
                        }
                    }
                }
            }
            else
            {
                try
                {
                    Query query = QueryBuilder.BuildQuery(indexerType, queryType, "keyword", keywords.Trim());
                    queries.Add(query);
                }
                catch (Exception e)
                {
                    queries = new List<Query>();
                }
            }

            return queries;
        }

        private void MapLanguageAndCategory(List<ResultData> results)
        {
            foreach(ResultData result in results)
            {
                result.Language = LanguageService.Get(int.Parse(result.Language)).Name;
                result.Category = CategoryService.Get(int.Parse(result.Category)).Name;
            }
        }

        public void Start()
        {
            EBookIndexer = new EBookIndexer(INDEX_DIR_PATH);
        }
    }
}