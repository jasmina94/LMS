using LMS.Infrastructure.Attributes.Implementation;
using LMS.Infrastructure.Validation;

namespace LMS.Models.ViewModels.Category
{
    public class CategoryViewModel : ViewModel
    {
        [RequiredProperty("Category name is required!")]
        public string Name { get; set; }

        [RequiredProperty("Category code is required!")]
        public string Code { get; set; }

        public override string Validate(ILMSValidator validator)
        {
            return validator.InvokeValidation(this);
        }
    }
}