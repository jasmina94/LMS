﻿using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Language
{
    public class LanguageViewModel : ViewModel
    {
        [RequiredProperty("Language name is required!")]
        public string Name { get; set; }

        [RequiredProperty("Language code is required!")]
        public string Code { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}