using System;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlTableHelper
   {
      public static MvcHtmlString TableField(this HtmlHelper htmlHelper, object value = null, object fieldId = null, string fieldClass = "", bool isHeader = false)
      {
         TagBuilder fieldTag;

         if (isHeader)
         {
            fieldTag = new TagBuilder("th");
         }
         else
         {
            fieldTag = new TagBuilder("td");
         }

         if (value != null)
         {
            if (value is DateTime)
            {
               var onlyDate = GetDateFromDateTime((DateTime)value);
               value = onlyDate;
            }
            fieldTag.InnerHtml = value.ToString();
         }
         CheckAndAssignAttribute("id", fieldTag, fieldId);
         CheckAndAssignAttribute("class", fieldTag, fieldClass);

         return new MvcHtmlString(fieldTag.ToString(TagRenderMode.Normal));
      }

      public static MvcHtmlString TableFieldWithLink(this HtmlHelper htmlHelper, object value = null, object fieldId = null, string fieldClass = "",
         string url = "", object linkId = null, string linkClass = "", string linkIcon = "")
      {
         TagBuilder fieldTag = new TagBuilder("td");
         TagBuilder linkTag = new TagBuilder("a");

         CheckAndAssignAttribute("class", fieldTag, fieldClass);
         CheckAndAssignAttribute("id", fieldTag, fieldId);

         if (!String.IsNullOrEmpty(linkIcon))
         {
            linkIcon = "glyphicon glyphicon-" + linkIcon;
         }
         if (!String.IsNullOrEmpty(linkClass))
         {
            linkClass = linkClass + " " + linkIcon;
         }
         linkTag.MergeAttribute("class", (linkClass != "") ? linkClass : linkIcon);

         CheckAndAssignAttribute("id", linkTag, linkId);
         CheckAndAssignAttribute("href", linkTag, url);

         if (value != null)
         {
            if (value is DateTime)
            {
               var onlyDate = GetDateFromDateTime((DateTime)value);
               value = onlyDate;
            }
            linkTag.InnerHtml = value.ToString();
         }
         fieldTag.InnerHtml = linkTag.ToString(TagRenderMode.Normal);

         return new MvcHtmlString(fieldTag.ToString(TagRenderMode.Normal));
      }

      private static void CheckAndAssignAttribute(string attributeToMerge, TagBuilder tag, object parameter)
      {
         if (parameter != null && !parameter.ToString().Equals(""))
         {
            tag.MergeAttribute(attributeToMerge, parameter.ToString());
         }
         else
         {
            if (attributeToMerge.Equals("href") && parameter.Equals(""))
            {
               tag.MergeAttribute(attributeToMerge, "#");
            }
         }
      }

      private static string GetDateFromDateTime(DateTime dateTime)
      {
         String dateOnlyFormatted = "";

         DateTime date = DateTime.Parse(dateTime.ToString());
         DateTime dateOnly = date.Date;

         dateOnlyFormatted = dateOnly.ToString("MM/dd/yyyy");

         return dateOnlyFormatted;
      }
   }
}