using LMS.Infrastructure.Attributes.Implementation;

namespace LMS.MVC.Infrastructure.Validation.Validators.Implementation
{
    public static class StringValidator
   {
      [RequiredProperty]
      public static bool NotNullNorEmpty(string value)
      {
         bool result = true;
         if (string.IsNullOrEmpty(value))
            result = false;
         return result;
      }

      [LengthProperty]
      public static bool LenghtInRange(int minLength, int maxLength, string value)
      {
         bool result = true;
         if(!string.IsNullOrEmpty(value))
         {
            if (value.Length < minLength || value.Length > maxLength)
            {
               result = false;
            }
         }
         
         return result;
      }
   }
}