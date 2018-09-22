using System;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlFormUtilHelper
   {
      public static MvcHtmlString FormSubmit(this HtmlHelper htmlHeleper, string value = "Save", bool newLine = false,
         string divClass = "form-group", string specialBtnClass = "lms-save-button", string buttonClass = "btn btn-default", string type="submit")
      {
         var divTag = new TagBuilder("div");
         var buttonTag = new TagBuilder("button");

         divTag.MergeAttribute("class", divClass);
         buttonTag.MergeAttribute("type", type);
         buttonTag.MergeAttribute("value", value);
         buttonTag.InnerHtml = value;

         if (!String.IsNullOrEmpty(specialBtnClass))
         {
            buttonClass = buttonClass + " " + specialBtnClass;
         }

         buttonTag.MergeAttribute("class", buttonClass);
         divTag.InnerHtml = Environment.NewLine + ((newLine) ? "<br />" : "") + buttonTag.ToString(TagRenderMode.Normal);

         return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
      }


      public static MvcHtmlString FormSubmitOrDelete(this HtmlHelper htmlHelper, string btnClass = "btn btn-default", bool newLine = false,
         string saveLabel = "Save", string deleteLabel = "Delete", string specialSaveBtnClass = "lms-save-button with-delete", string specialDeleteBtnClass = "lms-delete-button",
         string url = "")
      {

         var divTag = new TagBuilder("div");
         var saveBtnTag = new TagBuilder("button");
         var deleteBtnTag = new TagBuilder("button");
         var actionTag = new TagBuilder("a");

         divTag.MergeAttribute("class", "form-group pull-right");

         if (!String.IsNullOrEmpty(specialSaveBtnClass))
         {
            btnClass = btnClass + " " + specialSaveBtnClass;
         }

         saveBtnTag.MergeAttribute("class", btnClass);
         saveBtnTag.MergeAttribute("type", "submit");
         saveBtnTag.InnerHtml = saveLabel;

         if (!String.IsNullOrEmpty(specialDeleteBtnClass))
         {
            btnClass = "btn btn-default " + specialDeleteBtnClass;
         }

         deleteBtnTag.MergeAttribute("class", btnClass);
         deleteBtnTag.MergeAttribute("type", "button");
         deleteBtnTag.InnerHtml = deleteLabel;
         actionTag.MergeAttribute("href", url);
         actionTag.InnerHtml = deleteBtnTag.ToString();
         divTag.InnerHtml = Environment.NewLine + ((newLine) ? "<br />" : "") + saveBtnTag.ToString(TagRenderMode.Normal) +
                            Environment.NewLine + actionTag.ToString(TagRenderMode.Normal);

         return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
      }
   }
}