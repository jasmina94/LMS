using LMS.Infrastructure.Attributes.Implementation;
using System;

namespace LMS.MVC.Infrastructure.Validation.Validators.Implementation
{
    public static class DateTimeValidator
   {
      [RequiredProperty]
      public static bool DateTimeNotNull(DateTime value)
      {
         bool result = true;
         if (value == DateTime.MinValue)
            result = false;
         return result;
      }
   }
}