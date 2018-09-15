using System;

namespace LMS.Infrastructure.Attributes.Abstraction
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BasePropertyAttribute : Attribute, IPropertyAttribute
    {
        protected string validationMessage;
        protected AttributeType typeOfAttribute;

        public BasePropertyAttribute()
        {
            validationMessage = "ViewModel field is not valid";
            typeOfAttribute = AttributeType.Base;
        }

        public BasePropertyAttribute(string message)
        {
            validationMessage = message;
            typeOfAttribute = AttributeType.Base;
        }

        public AttributeType GetAttributeType()
        {
            return typeOfAttribute;
        }

        public string GetValidationMessage()
        {
            return validationMessage;
        }
    }
}