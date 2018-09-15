using LMS.DomainModel.Infrastructure.ORM.Attributes;
using System;

namespace LMS.DomainModel.DomainObject
{
    public class UserData : BaseData
    {
        [DBColumn("Firstname")]
        public string Firstname { get; set; }

        [DBColumn("Lastname")]
        public string Lastname { get; set; }

        [DBColumn("Username")]
        public string Username { get; set; }

        [DBColumn("UserPassword")]
        public string Password { get; set; }

        [DBColumn("BirthDate")]
        public DateTime BirthDate { get; set; }

        [DBColumn("Email")]
        public string Email { get; set; }

        [DBColumn("RefCategory")]
        public int RefCategory { get; set; }
    }
}
