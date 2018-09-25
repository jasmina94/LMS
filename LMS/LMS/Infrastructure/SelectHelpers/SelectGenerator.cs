using LMS.Models.ViewModels.Book;
using LMS.Models.ViewModels.Category;
using LMS.Models.ViewModels.Language;
using LMS.Models.ViewModels.User;
using System.Collections.Generic;

namespace LMS.MVC.Infrastructure.SelectHelpers
{
    public class SelectGenerator
    {
        public static SelectPageResult StringsToSelectPageResult(List<string> statusList, int statusCount)
        {
            SelectPageResult jsonItems = new SelectPageResult();
            jsonItems.Results = new List<SelectResult>();

            foreach (var item in statusList)
            {
                jsonItems.Results.Add(new SelectResult { id = item, text = item });
            }

            jsonItems.Total = statusCount;

            return jsonItems;
        }

        public static SelectPageResult UsersToSelectPageResult(List<UserViewModel> usersList, int usersCount)
        {
            SelectPageResult jsonUsers = new SelectPageResult();
            jsonUsers.Results = new List<SelectResult>();

            foreach (var item in usersList)
            {
                jsonUsers.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.Username.ToString() });
            }

            jsonUsers.Total = usersCount;

            return jsonUsers;
        }

        public static SelectPageResult CategoriesToSelectPageResult(List<CategoryViewModel> categoryList, int categoriesCount)
        {
            SelectPageResult jsonCategories = new SelectPageResult();
            jsonCategories.Results = new List<SelectResult>();

            foreach (var item in categoryList)
            {
                jsonCategories.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.Name.ToString() });
            }

            jsonCategories.Total = categoriesCount;

            return jsonCategories;
        }

        public static SelectPageResult LanguagesToSelectPageResult(List<LanguageViewModel> languageList, int languagesCount)
        {
            SelectPageResult jsonLanguages = new SelectPageResult();
            jsonLanguages.Results = new List<SelectResult>();

            foreach (var item in languageList)
            {
                jsonLanguages.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.Name.ToString() });
            }

            jsonLanguages.Total = languagesCount;

            return jsonLanguages;
        }

        public static SelectPageResult BooksToSelectPageResult(List<BookViewModel> bookList, int booksCount)
        {
            SelectPageResult jsonBooks = new SelectPageResult();
            jsonBooks.Results = new List<SelectResult>();

            foreach (var item in bookList)
            {
                jsonBooks.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.AuthorAndTitle.ToString() });
            }

            jsonBooks.Total = booksCount;

            return jsonBooks;
        }

        public static SelectPageResult ChatUsersToSelectPageResult(List<UserViewModel> usersList, int usersCount)
        {
            SelectPageResult jsonUsers = new SelectPageResult();
            jsonUsers.Results = new List<SelectResult>();

            foreach (var item in usersList)
            {
                jsonUsers.Results.Add(new SelectResult { id = item.Id.ToString() + " " + item.Username.ToString(), text = item.Firstname.ToString() + " " + item.Lastname.ToString() });
            }

            jsonUsers.Total = usersCount;

            return jsonUsers;
        }
    }
}