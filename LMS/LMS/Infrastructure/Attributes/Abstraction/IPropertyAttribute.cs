namespace LMS.Infrastructure.Attributes.Abstraction
{
    public interface IPropertyAttribute
    {
        string GetValidationMessage();

        AttributeType GetAttributeType();
    }
}
