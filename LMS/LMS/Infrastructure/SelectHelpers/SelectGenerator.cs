using LMS.DomainModel.DomainObject;
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

        public static SelectPageResult UsersToSelectPageResult(List<UserData> usersList, int usersCount)
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

        public static SelectPageResult ChatUsersToSelectPageResult(List<UserData> usersList, int usersCount)
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