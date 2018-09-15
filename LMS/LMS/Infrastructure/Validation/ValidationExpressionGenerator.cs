using LMS.Infrastructure.Attributes.Implementation;
using System.Linq.Expressions;
using System.Reflection;

namespace LMS.Infrastructure.Validation
{
    public static class ValidationExpressionGenerator
    {
        public static MethodCallExpression GenerateRequiredExpression(MethodInfo validationMethod, Expression valueToValidate)
        {
            MethodCallExpression validator = null;
            validator = Expression.Call(null, validationMethod, valueToValidate);

            return validator;
        }

        public static MethodCallExpression GenerateLengthRangeExpression(LengthPropertyAttribute attribute, MethodInfo validationMethod, Expression valueToValidate)
        {
            MethodCallExpression validator = null;
            ConstantExpression minLength = Expression.Constant(attribute.GetMinLength());
            ConstantExpression maxLength = Expression.Constant(attribute.GetMaxLength());

            var parameters = new Expression[] { minLength, maxLength, valueToValidate };
            validator = Expression.Call(null, validationMethod, parameters);

            return validator;
        }
    }
}