using System.Collections.Generic;

namespace LMS.Infrastructure.Authorization
{
    public class UserSessionObject
    {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int UserId { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Permissions { get; set; }
    }
}