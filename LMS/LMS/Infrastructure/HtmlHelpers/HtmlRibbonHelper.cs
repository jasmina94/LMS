using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Infrastructure.HtmlHelpers
{
    public static class HtmlRibbonHelper
   {
      public static IHtmlString Ribbon(this HtmlHelper htmlHeper, string typeName = "", string activeRibbonItemId = "",
         List<IHtmlString> ribbonItems = null, string className = "nav nav-pills ribbon")
      {
         MvcHtmlString mvcHtmlString = null;
         string htmlString = string.Empty;
         ribbonItems.RemoveAll(item => item == null);
         
         TagBuilder ribbon = BuildRibbon(className, typeName, ribbonItems, activeRibbonItemId);
         htmlString = ribbon.ToString();
         mvcHtmlString = new MvcHtmlString(htmlString);

         return mvcHtmlString;
      }

      private static TagBuilder BuildRibbon(string className, string typeName, List<IHtmlString> items, string activeId)
      {
         TagBuilder ribbon = new TagBuilder("ul");
         List<TagBuilder> ribbonItems = new List<TagBuilder>();
         bool hasActiveItem = items.Any(item => item.ToHtmlString().Contains(activeId));

         foreach (IHtmlString ribbonItem in items)
         {
            TagBuilder ribbonItemTag = BuildRibbonItem(ribbonItem, hasActiveItem, activeId);
            ribbonItems.Add(ribbonItemTag);
         }

         if (!hasActiveItem)
         {
            var activeRibbon = ribbonItems.First();
            activeRibbon.MergeAttribute("class", "active");
         }

         foreach (TagBuilder ribbonItem in ribbonItems)
         {
            ribbon.InnerHtml += Environment.NewLine + ribbonItem.ToString();
         }
         ribbon.MergeAttribute("class", className);
         ribbon.MergeAttribute("data-ribbon-type", typeName);
         
         return ribbon;
      }

      private static TagBuilder BuildRibbonItem(IHtmlString ribbonItem, bool isActive, string activeId)
      {
         TagBuilder ribbonItemTag = new TagBuilder("li");
         string item = ribbonItem.ToHtmlString();
         if (isActive && item.Contains(activeId))
         {
            ribbonItemTag.MergeAttribute("class", "active");
         }
         ribbonItemTag.InnerHtml = Environment.NewLine + ribbonItem.ToHtmlString();

         return ribbonItemTag;
      }

      public static IHtmlString RibbonItem(this HtmlHelper htmlHelper, string id, string label, string description, IHtmlString iconHtmlString = null)
      {
         TagBuilder tagBuilder = new TagBuilder("a");
         IDictionary<string, string> tagBuilderAttributes = tagBuilder.Attributes;
         MvcHtmlString mvcHtmlString = null;
         string htmlString = string.Empty;
         IHtmlString tooltip = htmlHelper.Tooltip(label, description);

         tagBuilderAttributes.Add("href", "#");
         tagBuilderAttributes.Add("class", "ribbon-item");
         tagBuilderAttributes.Add("data-ribbon-item", id);
         tagBuilderAttributes.Add("title", tooltip.ToHtmlString());

         if (iconHtmlString != null)
         {
            tagBuilder.InnerHtml = iconHtmlString.ToHtmlString();
         }
         else
         {
            tagBuilder.InnerHtml = label;
         }

         htmlString = tagBuilder.ToString();
         mvcHtmlString = new MvcHtmlString(htmlString);

         return mvcHtmlString;
      }
   }
}