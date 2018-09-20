using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;
using System;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [RequiredProperty("Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}