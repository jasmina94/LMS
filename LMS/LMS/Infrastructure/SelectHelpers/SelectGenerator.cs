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

        public static SelectPageResult CategoriesToSelectPageResult(List<CategoryViewModel> categoryList, int usersCount)
        {
            SelectPageResult jsonUsers = new SelectPageResult();
            jsonUsers.Results = new List<SelectResult>();

            foreach (var item in categoryList)
            {
                jsonUsers.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.Name.ToString() });
            }

            jsonUsers.Total = usersCount;

            return jsonUsers;
        }

        public static SelectPageResult LanguagesToSelectPageResult(List<LanguageViewModel> languageList, int usersCount)
        {
            SelectPageResult jsonUsers = new SelectPageResult();
            jsonUsers.Results = new List<SelectResult>();

            foreach (var item in languageList)
            {
                jsonUsers.Results.Add(new SelectResult { id = item.Id.ToString(), text = item.Name.ToString() });
            }

            jsonUsers.Total = usersCount;

            return jsonUsers;
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