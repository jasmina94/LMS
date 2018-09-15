using LMS.DomainModel.Infrastructure.ORM.Attributes;
using LMS.DomainModel.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LMS.DomainModel.Infrastructure.ORM
{
    public class ORMExpressionBuilder
    {
        private Dictionary<Type, MethodInfo> fromDBConversionMethods;
        private Dictionary<Type, MethodInfo> toDBConversionMethods;
        private string idName;

        public ORMExpressionBuilder(Dictionary<Type, MethodInfo> fromDBMethods, Dictionary<Type, MethodInfo> toDBMethods, Type modelType)
        {
            idName = NamingService.GetIdName(modelType);
            fromDBConversionMethods = fromDBMethods;
            toDBConversionMethods = toDBMethods;
        }

        public BlockExpression BuildDictionaryBlockExpression(ParameterExpression model, Type modelType)
        {
            BlockExpression block = null;
            List<Expression> expressionList = new List<Expression>();

            Type dictionaryType = typeof(Dictionary<string, object>);
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            ParameterExpression dictionary = Expression.Variable(dictionaryType, "command");
            expressionList.Add(Expression.Assign(dictionary, Expression.New(dictionaryType)));

            MethodInfo addMethod = dictionaryType.GetMethod("Add", bindingFlags, null, new[] { typeof(string), typeof(object) }, null);

            List<PropertyInfo> properties = GetPropertiesFromType(modelType);
            foreach (PropertyInfo propertyInfo in properties)
            {
                string keyName = GetMappingColumnName(propertyInfo, modelType);
                ConstantExpression key = Expression.Constant(keyName);

                MemberExpression value = Expression.Property(model, propertyInfo);
                UnaryExpression valueAsObject = Expression.Convert(value, typeof(object));

                expressionList.Add(Expression.Call(dictionary, addMethod, key, valueAsObject));
            }

            expressionList.Add(dictionary);
            block = Expression.Block(new[] { dictionary }, expressionList);

            return block;
        }

        public BlockExpression BuildDomainObjectBlockExpression(ParameterExpression dataRow, Type modelType)
        {
            BlockExpression block = null;
            List<Expression> expressionList = new List<Expression>();

            ParameterExpression domainModel = Expression.Variable(modelType, "domainModel");
            expressionList.Add(Expression.Assign(domainModel, Expression.New(modelType)));

            List<PropertyInfo> properties = GetPropertiesFromType(modelType);
            foreach (PropertyInfo propertyInfo in properties)
            {
                string mappingColumn = GetMappingColumnName(propertyInfo, modelType);
                MethodCallExpression conversionMethodCall = MakeConversionFromDB(propertyInfo, dataRow, mappingColumn);

                if (conversionMethodCall != null)
                {
                    Expression convertAndSetExpression = Expression.Call(domainModel, propertyInfo.GetSetMethod(), conversionMethodCall);
                    expressionList.Add(convertAndSetExpression);
                }
            }

            expressionList.Add(domainModel);
            block = Expression.Block(new[] { domainModel }, expressionList);

            return block;
        }

        public BlockExpression BuildSqlCommandBlockExpression(ParameterExpression model, Type modelType, string commandKind)
        {
            BlockExpression block = null;
            List<Expression> expressionList = new List<Expression>();
            Type commandType = typeof(SqlCommand);
            Type parameterCollectionType = typeof(SqlParameterCollection);
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            ParameterExpression command = Expression.Variable(commandType, "command");
            expressionList.Add(Expression.Assign(command, Expression.New(commandType)));

            PropertyInfo prop = commandType.GetProperty("Parameters", parameterCollectionType);
            MemberExpression left = Expression.Property(command, prop);

            MethodInfo addParamWithValue = parameterCollectionType.GetMethod("AddWithValue", bindingFlags, null, new[] { typeof(string), typeof(object) }, null);

            List<PropertyInfo> properties = GetPropertiesFromType(modelType);
            foreach (PropertyInfo property in properties)
            {
                string keyName = GetMappingColumnName(property, modelType);
                ConstantExpression key = Expression.Constant("@" + keyName);

                Expression value = MakeConversionToDB(property, model, commandKind);
                UnaryExpression valueAsObject = Expression.Convert(value, typeof(object));

                expressionList.Add(Expression.Call(left, addParamWithValue, key, valueAsObject));
            }

            expressionList.Add(command);
            block = Expression.Block(new[] { command }, expressionList);

            return block;
        }

        private List<PropertyInfo> GetPropertiesFromType(Type modelType)
        {
            Type attributeType = typeof(DBColumnAttribute);
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            List<PropertyInfo> properties = modelType.GetProperties(bindingFlags)
                  .Where(p => p.GetCustomAttributes(attributeType, false).Length > 0)
                  .ToList();

            return properties;
        }

        private string GetMappingColumnName(PropertyInfo property, Type modelType)
        {
            string mappingColumnName = "";
            object attribute = property.GetCustomAttributes(false)
                                       .Where(a => a is DBColumnAttribute)
                                       .First();

            mappingColumnName = ((DBColumnAttribute)attribute).ColumnName;
            if (mappingColumnName.Equals("Id"))
            {
                mappingColumnName = NamingService.GetIdName(modelType);
            }

            return mappingColumnName;
        }

        private MethodCallExpression MakeConversionFromDB(PropertyInfo property, ParameterExpression dataRow, string mappingColumn)
        {
            MethodCallExpression conversionCallExpression = null;
            MethodInfo conversionMethod = null;
            Type propertyType = property.PropertyType;

            if (fromDBConversionMethods.TryGetValue(propertyType, out conversionMethod))
            {
                IndexExpression dataBaseValue = Expression.Property(dataRow, "Item", new Expression[] { Expression.Constant(mappingColumn) });
                conversionCallExpression = Expression.Call(null, conversionMethod, dataBaseValue);
            }

            return conversionCallExpression;
        }

        private MethodCallExpression MakeConversionToDB(PropertyInfo property, ParameterExpression model, string commandType)
        {
            MethodCallExpression converionCallExpression = null;
            MethodInfo conversionMethod = null;
            Expression propertyValue = null;
            Type propertyType = property.PropertyType;
            String propertyName = property.Name;

            propertyValue = Expression.Call(model, property.GetGetMethod());

            if (commandType.Equals(ORMConstant.Insert))
            {
                propertyValue = PrepareValuesForInsert(propertyValue, propertyName);
            }

            if (toDBConversionMethods.TryGetValue(propertyType, out conversionMethod))
            {
                converionCallExpression = Expression.Call(null, conversionMethod, propertyValue);
            }
            else
            {
                converionCallExpression = (MethodCallExpression)propertyValue;
            }

            return converionCallExpression;
        }

        private Expression PrepareValuesForInsert(Expression propertyValue, string propertyName)
        {
            if (propertyName.Equals(ORMConstant.IsActiveProperty))
            {
                propertyValue = Expression.Constant(true);
            }

            if (propertyName.Equals(ORMConstant.DateCreationProperty))
            {
                propertyValue = Expression.Constant(DateTime.Now);
            }

            return propertyValue;
        }
    }
}
