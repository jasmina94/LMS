using LMS.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models.ViewModels
{
    public abstract class ViewModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public int RefUserCreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTimeCreatedOn { get; set; }

        public int RefUserDeletedBy { get; set; }

        public DateTime DateTimeDeletedOn { get; set; }

        public abstract string Validate(ILMSValidator validator);
    }
}