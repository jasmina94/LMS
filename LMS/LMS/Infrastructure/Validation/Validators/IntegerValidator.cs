using LMS.Infrastructure.Attributes.Implementation;

namespace LMS.MVC.Infrastructure.Validation.Validators.Implementation
{
    public static class IntegerValidator
    {
        [RequiredProperty]
        public static bool NotNull(int value)
        {
            bool result = true;
            if (value == 0)
                result = false;
            return result;
        }
    }
}