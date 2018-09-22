using System;
using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.Models.ViewModels.Relation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.Services.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserBookCopy;
using LMS.DomainModel.DomainObject.Relation;
using LMS.Models.ViewModels.Book;
using LMS.BusinessLogic.UserManagement.Interfaces;

namespace LMS.BusinessLogic.BookManagement.Implementation
{
    public class LoanServiceImpl : ILoanService
    {
        public IRelationUserBookCopyRepository RelationUserBookCopyRepository { get; set; }

        public IBookRepository BookRepository { get; set; }

        public IBookCopyRepository BookCopyRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public IBookService BookService { get; set; }

        public IUserService UserService { get; set; }

        public RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId)
        {
            var viewModel = new RelationUserBookCopyViewModel
            {
                BookCopyId = bookCopyId,
                DateOfIssue = DateTime.Now,
                DateDueForReturn = DateTime.Now.AddDays(14)
            };

            return viewModel;
        }

        public BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel)
        {
            var result = new BorrowResult();

            viewModel.DateOfIssue = DateTime.Now;
            viewModel.DateDueForReturn = DateTime.Now.AddDays(14);

            RelationUserBookCopyDomainModelBuilder builder = BuilderResolverService.Get
                <RelationUserBookCopyDomainModelBuilder, RelationUserBookCopyViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            RelationUserBookCopyData domainModel = builder.GetDataModel();

            int loanId = RelationUserBookCopyRepository.SaveData(domainModel);
            if (loanId != 0)
            {
                var saveResult = UpdateCopyAndBook(domainModel.BookCopyId);
                if (saveResult.Success)
                {
                    string username = UserService.Get(domainModel.UserId).Username;
                    result = new BorrowResult(loanId, domainModel.BookCopyId, username);
                }
            }

            return result;
        }

        private SaveBookResult UpdateCopyAndBook(int copyId)
        {
            var result = new SaveBookResult();
            BookCopyViewModel bookCopyData = BookService.GetCopy(copyId);
            bookCopyData.Available = false;

            result = BookService.SaveOnly(bookCopyData);

            if (result.Success)
            {
                BookViewModel bookData = BookService.Get(bookCopyData.BookId);
                bookData.NumOfAvailableCopies = bookData.NumOfAvailableCopies - 1;

                result = BookService.Save(bookData);
            }

            return result;
        }
    }
}