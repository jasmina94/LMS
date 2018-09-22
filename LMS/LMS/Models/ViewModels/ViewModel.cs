using LMS.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.ViewModels
{
    public abstract class ViewModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public int UserCreatedById { get; set; }

        public string UserCreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTimeCreatedOn { get; set; }

        public int UserDeletedById { get; set; }

        public string UserDeletedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTimeDeletedOn { get; set; }

        public bool IsNew
        {
            get
            {
                return Id != 0 ? false : true;
            }
        }

        public abstract string Validate(ILMSValidator validator);
    }
}