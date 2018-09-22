using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlSidebarHelper
    {
        public static IHtmlString SidebarItem(this HtmlHelper htmlHelper, string name, string label,
            string description, IHtmlString iconHtmlString = null, string style = null)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            IDictionary<string, string> tagBuilderAttributes = tagBuilder.Attributes;
            MvcHtmlString mvcHtmlString = null;
            string htmlString = string.Empty;
            IHtmlString tooltip = htmlHelper.Tooltip(label, description);

            tagBuilderAttributes.Add("href", "#");
            tagBuilderAttributes.Add("class", "btn sidebar-action");
            tagBuilderAttributes.Add("role", "button");
            tagBuilderAttributes.Add("data-sidebar-action", name);
            tagBuilderAttributes.Add("title", tooltip.ToHtmlString());
            
            if (iconHtmlString != null)
                tagBuilder.InnerHtml = iconHtmlString.ToHtmlString();

            if (!string.IsNullOrEmpty(style))
                tagBuilderAttributes.Add("id", style);

            htmlString = tagBuilder.ToString();
            mvcHtmlString = new MvcHtmlString(htmlString);

            return mvcHtmlString;
        }
    }
}