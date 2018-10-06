using LMS.Infrastructure.HtmlHelpers.Utility;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlFormInputHelper
    {
        public static MvcHtmlString SimpleFormInput<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression = null,
          string labelValue = "Label: ", string divClass = "form-group", string labelClass = "control-label col-xs-3", string inputClass = "form-control col-xs-3",
          bool readOnly = false, string inputType = "", string inputValue = "", string inputId = "", string inputName = "", string specialInputClass = "")
        {
            TagBuilder divTag = new TagBuilder("div");
            TagBuilder labelTag = new TagBuilder("label");
            TagBuilder inputTag = new TagBuilder("input");
            MemberExpression body = null;

            if (expression != null)
            {
                body = expression.Body as MemberExpression;
                if(body == null)
                    body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            
            if (body != null)
            {
                inputId = body.Member.Name.ToLower();
                inputName = body.Member.Name;
            }

            divTag.MergeAttribute("class", divClass);
            labelTag.MergeAttribute("class", labelClass);
            labelTag.MergeAttribute("for", inputId);
            labelTag.InnerHtml = labelValue;

            TypeResolver.ResolveInputTypeForTag(inputType, inputTag);

            if (!string.IsNullOrEmpty(specialInputClass))
            {
                inputClass = inputClass + " " + specialInputClass;
            }

            inputTag.MergeAttribute("class", inputClass);
            inputTag.MergeAttribute("id", inputId);
            inputTag.MergeAttribute("name", inputName);
            inputTag.MergeAttribute("value", inputValue);

            if (readOnly)
            {
                inputTag.MergeAttribute("readonly", "readonly");
            }

            divTag.InnerHtml = Environment.NewLine + labelTag.ToString() +
                               Environment.NewLine + inputTag.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString CheckboxFormInput<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression = null, string labelValue = "Label: ")
        {
            TagBuilder divTag = new TagBuilder("div");
            TagBuilder labelTag = new TagBuilder("label");
            TagBuilder checkboxInputTag = new TagBuilder("input");
            TagBuilder hiddenInputTag = new TagBuilder("input");

            string inputId = "";
            string inputName = "";

            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            inputId = body.Member.Name.ToLower();
            inputName = body.Member.Name;

            divTag.MergeAttribute("class", "checkbox");

            checkboxInputTag.MergeAttribute("type", "checkbox");
            checkboxInputTag.MergeAttribute("id", inputId);
            checkboxInputTag.MergeAttribute("name", inputName);
            checkboxInputTag.MergeAttribute("data-val", "true");
            checkboxInputTag.MergeAttribute("value", "true");

            hiddenInputTag.MergeAttribute("name", inputName);
            hiddenInputTag.MergeAttribute("hidden", "");
            hiddenInputTag.MergeAttribute("value", "false");


            labelTag.InnerHtml = checkboxInputTag.ToString() + labelValue + Environment.NewLine;

            divTag.InnerHtml = Environment.NewLine + labelTag.ToString(TagRenderMode.Normal) +
                               Environment.NewLine + hiddenInputTag.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
        }
    }
}