using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlErrorLabelHelper
   {
      public static MvcHtmlString ErrorLabel(this HtmlHelper htmlHelper, string value = "", string errorClass = "lms-error-label")
      {
         TagBuilder errorTag = new TagBuilder("p");
         TagBuilder boldTag = new TagBuilder("b");        
         
         errorTag.MergeAttribute("class", errorClass);

         boldTag.InnerHtml = value;
         errorTag.InnerHtml = boldTag.ToString();

         return new MvcHtmlString(errorTag.ToString(TagRenderMode.Normal));
      }
   }
}