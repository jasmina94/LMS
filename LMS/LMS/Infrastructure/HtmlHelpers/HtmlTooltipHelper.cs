using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlTooltipHelper
   {
      public static IHtmlString Tooltip(this HtmlHelper htmlHelper, string title, string description)
      {
         TagBuilder containerTagBuilder = new TagBuilder("div");
         StringBuilder titleStringBuilder = new StringBuilder();
         StringBuilder descriptionStringBuilder = new StringBuilder();
         MvcHtmlString mvcHtmlString = null;
         string htmlString = string.Empty;

         titleStringBuilder
            .Append("<p><strong>")
            .Append(title)
            .Append("</p></strong>");

         descriptionStringBuilder
            .Append("<p><em>")
            .Append(description)
            .Append("</p></em>");

         containerTagBuilder.InnerHtml = titleStringBuilder.ToString() + descriptionStringBuilder.ToString();
         htmlString = containerTagBuilder.ToString();
         mvcHtmlString = new MvcHtmlString(htmlString);

         return mvcHtmlString;
      }
   }
}