﻿using LMS.BusinessLogic.BookManagement.Interfaces;
using LMS.BusinessLogic.BookManagement.Model;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.ModelBuilders.Implementation.Relation.UserBookCopy;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Relation;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;
using LMS.Services.Interfaces;
using System.Linq;
using System;

namespace LMS.BusinessLogic.BookManagement.Implementation
{
    public class LoanServiceImpl : ILoanService
    {
        #region Injected properties
        public IRelationUserBookCopyRepository RelationUserBookCopyRepository { get; set; }

        public IBookRepository BookRepository { get; set; }

        public IBookCopyRepository BookCopyRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        public IBookService BookService { get; set; }

        public IUserService UserService { get; set; }
        #endregion

        public RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId)
        {
            BookCopyData copy = BookCopyRepository.GetDataById(bookCopyId);
            BookData book = BookRepository.GetDataById(copy.BookId);

            var viewModel = new RelationUserBookCopyViewModel
            {
                BookCopyId = bookCopyId,
                BookFullName = book.BookAuthorAndTitle,
                DateOfIssue = DateTime.Now,
                DateDueForReturn = DateTime.Now.AddDays(14)
            };

            return viewModel;
        }

        public BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel, UserSessionObject user)
        {
            var result = new BorrowResult();

            viewModel.DateOfIssue = DateTime.Now;
            viewModel.DateDueForReturn = DateTime.Now.AddDays(14);

            RelationUserBookCopyDomainModelBuilder builder = BuilderResolverService.Get
                <RelationUserBookCopyDomainModelBuilder, RelationUserBookCopyViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            RelationUserBookCopyData domainModel = builder.GetDataModel();

            if(viewModel.Id == 0)
                domainModel.RefUserCreatedBy = user.UserId;

            int loanId = RelationUserBookCopyRepository.SaveData(domainModel);
            if (loanId != 0)
            {
                var saveResult = UpdateCopyAndBook(domainModel.BookCopyId, user);
                if (saveResult.Success)
                {
                    string username = UserService.Get(domainModel.UserId).Username;
                    result = new BorrowResult(loanId, domainModel.BookCopyId, username);
                }
            }

            return result;
        }

        private SaveBookResult UpdateCopyAndBook(int copyId, UserSessionObject user)
        {
            var result = new SaveBookResult();

            BookCopyViewModel bookCopyData = BookService.GetCopy(copyId);
            bookCopyData.Available = false;

            result = BookService.SaveOnly(bookCopyData, user);

            if (result.Success)
            {
                BookViewModel bookData = BookService.Get(bookCopyData.BookId);
                bookData.NumOfAvailableCopies = bookData.NumOfAvailableCopies - 1;

                result = BookService.Save(bookData, user);
            }

            return result;
        }

        public List<RelationUserBookCopyViewModel> GetLoans(bool active)
        {
            var viewModels = new List<RelationUserBookCopyViewModel>();
            var domainModels = new List<RelationUserBookCopyData>();

            if (active)
            {
                domainModels = RelationUserBookCopyRepository.GetAllActiveData();
            }
            else
            {
                domainModels = RelationUserBookCopyRepository.GetAllData();
            }
           
            viewModels = ConvertToViewModels(domainModels);

            return viewModels;
        }

        public List<RelationUserBookCopyViewModel> GetLoansForUser(bool active, int userId)
        {
            List<RelationUserBookCopyViewModel> userLoans = new List<RelationUserBookCopyViewModel>();
            List<RelationUserBookCopyViewModel> all = GetLoans(active);

            userLoans = all.Where(x => x.UserId == userId).ToList();

            return userLoans;
        }

        private List<RelationUserBookCopyViewModel> ConvertToViewModels(List<RelationUserBookCopyData> domainModels)
        {
            var viewModels = new List<RelationUserBookCopyViewModel>();

            foreach (var item in domainModels)
            {
                RelationUserBookCopyViewModelBuilder builder = BuilderResolverService.Get
                    <RelationUserBookCopyViewModelBuilder, RelationUserBookCopyData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        public BorrowResult ReturnBook(int loandId, UserSessionObject user)
        {
            var result = new BorrowResult();
            RelationUserBookCopyData loanData = RelationUserBookCopyRepository.GetDataById(loandId);

            if(loanData != null)
            {
                loanData.DateReturned = DateTime.Now;
                loanData.DateTimeDeletedOn = DateTime.Now;
                loanData.RefUserDeletedBy = user.UserId;
                loanData.IsActive = false;
                      
                BookCopyData bookCopy = BookCopyRepository.GetDataById(loanData.BookCopyId);             
                if(bookCopy != null)
                {
                    bookCopy.OnLoan = false;
                    BookData book = BookRepository.GetDataById(bookCopy.BookId);
                    if(book != null)
                    {
                        book.NumOfAvailableCopies = book.NumOfAvailableCopies + 1;

                        int bookId = BookRepository.SaveData(book);
                        int bookCopyId = BookCopyRepository.SaveData(bookCopy);
                        int loanId = RelationUserBookCopyRepository.SaveData(loanData);

                        result = new BorrowResult(loandId, bookCopyId);
                    }
                    else
                    {
                        result.Message = "Book not found.";
                    }
                }
                else
                {
                    result.Message = "Book copy not found.";
                }               
            }
            else
            {
                result.Message = "Loan with id " + loandId + "doesn't exist.";
            }

            return result;
        }
    }
}