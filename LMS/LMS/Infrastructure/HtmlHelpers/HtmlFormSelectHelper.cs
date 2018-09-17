using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlFormSelectHelper
    {
        public static MvcHtmlString DropDownSelect<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression = null,
           string labelValue = "Label: ", string divClass = "form-group", string labelClass = "control-label col-xs-3", string innerDivClass = "col-xs-7",
           string selectClass = "form-control", string selectName = "", string selectId = "", int selectValue = 0,
           string innerDivSpecialClass = "", string selectionSource = "", string value = "", bool edit = false)
        {
            StringBuilder builder = null;
            MemberExpression body = null;
            var divTag = new TagBuilder("div");
            var labelTag = new TagBuilder("label");
            var innerDivTag = new TagBuilder("div");
            var selectTag = new TagBuilder("select");

            if (expression != null)
            {
                body = expression.Body as MemberExpression;
                if (body == null)
                    body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            if (body != null)
            {
                selectId = body.Member.Name.ToLower();
                selectName = body.Member.Name;
            }

            builder = PrepareSelectionSource(edit, selectionSource, value);

            divTag.MergeAttribute("class", divClass);
            labelTag.MergeAttribute("class", labelClass);
            labelTag.MergeAttribute("for", selectId);
            labelTag.InnerHtml = labelValue;

            innerDivTag.MergeAttribute("class", (String.IsNullOrEmpty(innerDivSpecialClass)) ? innerDivClass : innerDivClass + " " + innerDivSpecialClass);
            innerDivTag.MergeAttribute("id", builder.ToString());

            selectTag.MergeAttribute("class", selectClass);
            selectTag.MergeAttribute("name", selectName);
            selectTag.MergeAttribute("id", selectId);

            innerDivTag.InnerHtml = Environment.NewLine + selectTag.ToString(TagRenderMode.Normal);
            divTag.InnerHtml = Environment.NewLine + labelTag.ToString(TagRenderMode.Normal) +
                                 innerDivTag.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
        }

        private static StringBuilder PrepareSelectionSource(bool edit, string selectionSource, string value)
        {
            StringBuilder builder = new StringBuilder();
            if (edit)
            {
                builder.Append(selectionSource);
                builder.Append("&");
                builder.Append(value);
            }
            else
            {
                builder.Append(selectionSource);
            }

            return builder;
        }

        public static IHtmlString SearchUsers(this HtmlHelper htmlHelper, string divClass = "input-group", string selectClass = "form-control",
            string placeholder = "Search:", string spanClass = "input-group-addon", string iconClass = "glyphicon glyphicon-search",
            string divSpecialClass = "", string selectSpecialClass = "", string selectSource = "")
        {
            MvcHtmlString mvcHtmlString = null;
            string htmlString = string.Empty;
            string selectId = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            TagBuilder wrapper = new TagBuilder("div");
            TagBuilder searchInput = new TagBuilder("select");
            TagBuilder span = new TagBuilder("span");
            TagBuilder icon = new TagBuilder("i");

            icon.MergeAttribute("class", iconClass);
            span.MergeAttribute("class", spanClass);
            span.InnerHtml = icon.ToString(TagRenderMode.Normal);

            stringBuilder.Append(placeholder);
            stringBuilder.Append("&");
            stringBuilder.Append(selectSource);
            selectId = stringBuilder.ToString();

            stringBuilder.Clear();
            stringBuilder.Append(selectClass);
            stringBuilder.Append(string.Empty);
            stringBuilder.Append(selectSpecialClass);
            selectClass = stringBuilder.ToString();

            searchInput.MergeAttribute("class", selectClass);
            searchInput.MergeAttribute("placeholder", placeholder);
            searchInput.MergeAttribute("id", selectId);

            wrapper.InnerHtml = span.ToString(TagRenderMode.Normal) + Environment.NewLine + searchInput.ToString(TagRenderMode.Normal);
            htmlString = wrapper.ToString();

            mvcHtmlString = new MvcHtmlString(htmlString);

            return mvcHtmlString;
        }
    }
}