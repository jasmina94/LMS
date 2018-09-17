using System.Web.Mvc;

namespace LMS.Infrastructure.HtmlHelpers.Utility
{
    public class TypeResolver
    {
        public static TagBuilder ResolveIconTypeForTag(string iconType, TagBuilder tag)
        {
            switch (iconType)
            {
                case IconType.CALENDAR:
                    tag.MergeAttribute("class", "glyphicon glyphicon-" + IconType.CALENDAR);
                    break;
                case IconType.TIME:
                    tag.MergeAttribute("class", "glyphicon glyphicon-" + IconType.TIME);
                    break;
                default:
                    tag.MergeAttribute("class", "");
                    break;
            }

            return tag;
        }

        public static TagBuilder ResolveInputTypeForTag(string inputType, TagBuilder tag)
        {
            switch (inputType)
            {
                case FormInputType.NUMBER:
                    tag.MergeAttribute("type", FormInputType.NUMBER);
                    break;
                case FormInputType.EMAIL:
                    tag.MergeAttribute("type", FormInputType.EMAIL);
                    break;
                case FormInputType.PASSWORD:
                    tag.MergeAttribute("type", FormInputType.PASSWORD);
                    break;
                case FormInputType.HIDDEN:
                    tag.MergeAttribute("type", FormInputType.HIDDEN);
                    break;
                case FormInputType.CHECKBOX:
                    tag.MergeAttribute("type", FormInputType.CHECKBOX);
                    break;
                case FormInputType.RADIO:
                    tag.MergeAttribute("type", FormInputType.RADIO);
                    break;
                default:
                    tag.MergeAttribute("type", FormInputType.TEXT);
                    break;
            }

            return tag;
        }
    }
}