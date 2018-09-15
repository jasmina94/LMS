using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.ViewModels.Relation
{
    public class RelationUserBookCopyViewModel : ViewModel
    {
        [RequiredProperty("Book copy is required!")]
        public int BookCopyId { get; set; }

        [RequiredProperty("User is required!")]
        public int UserId { get; set; }

        [RequiredProperty("Date of issue is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }

        [RequiredProperty("Date due for return is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDueForReturn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateReturned { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}