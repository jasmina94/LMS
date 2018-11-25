﻿using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Book;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface IEBookService
    {
        BookViewModel Get(int? bookId);

        List<BookViewModel> GetAll(bool isActive);

        BookViewModel LoadBaseFromFile(string filePath, string fileName);

        SaveEBookResult SaveAndIndex(EBookCreateViewModel viewModel, string fullPath, UserSessionObject user);

        bool Delete(int bookId, string path, int userId);
    }
}