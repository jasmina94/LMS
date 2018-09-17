using LMS.Infrastructure.Attributes.Implementation;
using System;

namespace LMS.MVC.Infrastructure.Validation.Validators.Implementation
{
    public static class TimeSpanValidator
   {
      [RequiredProperty]
      public static bool DateTimeNotNull(TimeSpan value)
      {
         bool result = true;
         if (value == null)
            result = false;
         return result;
      }
   }
}