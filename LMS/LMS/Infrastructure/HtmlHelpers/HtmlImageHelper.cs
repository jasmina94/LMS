using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlImageHelper
   {
      public static IHtmlString Icon(this HtmlHelper htmlHelper, string classes)
      {
         TagBuilder tagBuilder = new TagBuilder("span");
         IDictionary<string, string> tagBuilderAttributes = tagBuilder.Attributes;
         MvcHtmlString mvcHtmlString = null;
         string htmlString = string.Empty;

         tagBuilderAttributes.Add("class", classes);
         htmlString = tagBuilder.ToString();
         mvcHtmlString = new MvcHtmlString(htmlString);

         return mvcHtmlString;
      }

      public static IHtmlString Glyphicon(this HtmlHelper htmlHelper, string name, bool forInput = false)
      {
         string glyphiconClass = "glyphicon glyphicon-" + name;
         IHtmlString htmlString;

         if (forInput)
         {
            htmlString = InputIcon(htmlHelper, glyphiconClass);
         }
         else
         {
            htmlString = Icon(htmlHelper, glyphiconClass);
         }

         return htmlString;
      }

      public static IHtmlString InputIcon(this HtmlHelper htmlHelper, string classes)
      {
         TagBuilder spanTag = new TagBuilder("span");
         TagBuilder iconTag = new TagBuilder("i");

         spanTag.MergeAttribute("class", "input-group-addon");
         iconTag.MergeAttribute("class", classes);

         spanTag.InnerHtml = iconTag.ToString();

         return new MvcHtmlString(spanTag.ToString());
      }
   }
}