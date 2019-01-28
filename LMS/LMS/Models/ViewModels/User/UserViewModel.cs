using LMS.Infrastructure.Attributes.Implementation;
using System.ComponentModel.DataAnnotations;
using LMS.Infrastructure.Validation;
using System;


namespace LMS.Models.ViewModels.User
{
    public class UserViewModel : ViewModel
    {
        [RequiredProperty("Firstname is required!")]
        public string Firstname { get; set; }

        [RequiredProperty("Lastname is required!")]
        public string Lastname { get; set; }

        [RequiredProperty("Username is required!")]
        public string Username { get; set; }

        [RequiredProperty("Password is required!")]
        public string UserPassword { get; set; }

        [RequiredProperty("Date of birth is required!")]
        public DateTime BirthDate { get; set; }

        [RequiredProperty("Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        [RequiredProperty("Role is required!")]
        public int RoleId { get; set; }

        public string Role { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}