using LMS.Infrastructure.HtmlHelpers.Utility;
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers
{
    public static class HtmlFormInputIconHelper
   {
      public static MvcHtmlString IconFormInput<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression,
         string labelValue = "Label: ", string inputType = FormInputType.TEXT, bool readOnly = false, string divClass = "form-group",
         string labelClass = "control-label col-xs-3", string inputClass = "form-control col-xs-3", string inputValue = "", string inputId = "",
         string inputName = "", string specialInputClass = "", IHtmlString iconHtmlString = null)
      {
         var divTag = new TagBuilder("div");
         var labelTag = new TagBuilder("label");
         var inputTag = new TagBuilder("input");
         var divInnerTag = new TagBuilder("div");
         var body = expression.Body as MemberExpression;

         if (body == null)
         {
            body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
         }

         inputId = body.Member.Name.ToLower();
         inputName = body.Member.Name;
         divTag.MergeAttribute("class", divClass);
         labelTag.MergeAttribute("class", labelClass);
         labelTag.MergeAttribute("for", inputId);
         labelTag.InnerHtml = labelValue;

         TypeResolver.ResolveInputTypeForTag(inputType, inputTag);

         if (!String.IsNullOrEmpty(specialInputClass))
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

         divInnerTag.MergeAttribute("class", "input-group");

         if (iconHtmlString != null)
         {
            divInnerTag.InnerHtml = iconHtmlString.ToHtmlString() +
                                    Environment.NewLine + inputTag.ToString(TagRenderMode.SelfClosing);
         }
         else
         {
            divInnerTag.InnerHtml = Environment.NewLine + inputTag.ToString(TagRenderMode.SelfClosing);
         }

         divTag.InnerHtml = Environment.NewLine + labelTag.ToString() +
                            Environment.NewLine + divInnerTag.ToString(TagRenderMode.Normal);

         return new MvcHtmlString(divTag.ToString(TagRenderMode.Normal));
      }
   }
}