using System.Collections.Generic;

namespace LMS.MVC.Infrastructure.SelectHelpers
{
    public class SelectPageResult
   {
      public int Total { get; set; }

      public List<SelectResult> Results { get; set; }
   }
}