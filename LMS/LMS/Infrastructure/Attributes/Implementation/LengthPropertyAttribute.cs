using LMS.Infrastructure.Attributes.Abstraction;

namespace LMS.Infrastructure.Attributes.Implementation
{
    public class LengthPropertyAttribute : BasePropertyAttribute, IPropertyAttribute
    {
        private int minLength;
        private int maxLength;

        public LengthPropertyAttribute()
        {
            typeOfAttribute = AttributeType.RangeLength;
        }

        public LengthPropertyAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
            validationMessage = string.Format("Field length must be in range from {0} to {1}.", this.minLength, this.maxLength);
            typeOfAttribute = AttributeType.RangeLength;
        }

        public LengthPropertyAttribute(int minLength, int maxLength, string message)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
            validationMessage = message;
            typeOfAttribute = AttributeType.RangeLength;
        }

        public int GetMinLength()
        {
            return minLength;
        }

        public int GetMaxLength()
        {
            return maxLength;
        }
    }
}