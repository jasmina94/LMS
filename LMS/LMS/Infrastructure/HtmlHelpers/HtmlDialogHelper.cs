using LMS.Infrastructure.HtmlHelpers.Utility;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlDialogHelper
    {
        public static MvcHtmlString DialogTitle(this HtmlHelper htmlHelper, string name = "title", 
            string titleClass = "TitleDialog", string inputType = FormInputType.HIDDEN, string value = "")
        {
            var inputTag = new TagBuilder("input");

            inputTag.MergeAttribute("name", name);
            inputTag.MergeAttribute("value", value);
            inputTag.MergeAttribute("class", titleClass);
            inputTag.MergeAttribute("type", inputType);

            return new MvcHtmlString(inputTag.ToString(TagRenderMode.SelfClosing));
        }
    }
}