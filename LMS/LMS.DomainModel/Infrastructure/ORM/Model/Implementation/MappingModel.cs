using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.ORM.Model.Interfaces;
using LMS.DomainModel.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LMS.DomainModel.Infrastructure.ORM.Model.Implementation
{
    public class MappingModel<T> : IMappingModel where T : class
    {
        private Func<T, Dictionary<string, object>> dictionaryStatement;
        private Func<DataRow, T> conversionStatement;
        private Func<T, SqlCommand> updateCommandStatement;
        private Func<T, SqlCommand> insertCommandStatement;
        private ORMExpressionBuilder expressionBuilder;
        public BaseData Model;

        public MappingModel(Dictionary<Type, MethodInfo> fromDBMethods, Dictionary<Type, MethodInfo> toDBMethods)
        {
            Type modelType = typeof(T);
            expressionBuilder = new ORMExpressionBuilder(fromDBMethods, toDBMethods, modelType);

            InitializeStatements();
        }

        private void InitializeStatements()
        {
            dictionaryStatement = MakeDictionaryStatement();
            conversionStatement = MakeConversionStatement();
            updateCommandStatement = MakeCommandStatement(ORMConstant.Update);
            insertCommandStatement = MakeCommandStatement(ORMConstant.Insert);
        }

        private Func<T, Dictionary<string, object>> MakeDictionaryStatement()
        {
            Func<T, Dictionary<string, object>> predicate = null;
            Type modelType = typeof(T);

            ParameterExpression model = Expression.Parameter(modelType, "modelType");
            BlockExpression block = expressionBuilder.BuildDictionaryBlockExpression(model, modelType);

            Expression<Func<T, Dictionary<string, object>>> lambda = Expression.Lambda<Func<T, Dictionary<string, object>>>(block, model);
            predicate = lambda.Compile();

            return predicate;
        }

        private Func<DataRow, T> MakeConversionStatement()
        {
            Func<DataRow, T> predicate = null;
            Type modelType = typeof(T);
            Type dataRowType = typeof(DataRow);

            ParameterExpression dataRow = Expression.Parameter(dataRowType, "dataRow");
            BlockExpression block = expressionBuilder.BuildDomainObjectBlockExpression(dataRow, modelType);

            Expression<Func<DataRow, T>> lambda = Expression.Lambda<Func<DataRow, T>>(block, dataRow);
            predicate = lambda.Compile();

            return predicate;
        }

        private Func<T, SqlCommand> MakeCommandStatement(string commandType)
        {
            Func<T, SqlCommand> predicate = null;
            Type modelType = typeof(T);

            ParameterExpression model = Expression.Parameter(modelType, "modelType");
            BlockExpression block = expressionBuilder.BuildSqlCommandBlockExpression(model, modelType, commandType);

            Expression<Func<T, SqlCommand>> lambda = Expression.Lambda<Func<T, SqlCommand>>(block, model);
            predicate = lambda.Compile();

            return predicate;
        }

        public T ExecuteConversion(DataRow dataRow)
        {
            T data = conversionStatement(dataRow);

            return data;
        }

        public SqlCommand ExecuteCommand(string commandType)
        {
            SqlCommand command;
            string query = "";

            if (commandType.Equals(ORMConstant.Insert))
            {
                command = insertCommandStatement((T)Convert.ChangeType(Model, typeof(T)));
                query = ExecuteInsertQuery();
            }
            else
            {
                command = updateCommandStatement((T)Convert.ChangeType(Model, typeof(T)));
                query = ExecuteUpdateQuery();
            }

            command.CommandText = query;

            return command;
        }

        private string ExecuteInsertQuery()
        {
            string query = "";
            Dictionary<string, object> dict = Execute();
            query = MakeInsertQuery(dict);

            return query;
        }

        private string ExecuteUpdateQuery()
        {
            string query = "";
            Dictionary<string, object> dict = Execute();
            query = MakeUpdateQuery(dict);

            return query;

        }

        private Dictionary<string, object> Execute()
        {
            Dictionary<string, object> dictionary = dictionaryStatement((T)Convert.ChangeType(Model, typeof(T)));

            return dictionary;
        }

        private string MakeInsertQuery(Dictionary<string, object> dictionary)
        {
            string query = "";
            StringBuilder builderInsert = new StringBuilder();
            StringBuilder builderValues = new StringBuilder();

            string tableName = NamingService.GetTableNameFromDomainModel(typeof(T));

            builderInsert.Append(string.Format("INSERT INTO {0} (", tableName));
            builderValues.Append("VALUES (");

            foreach (var item in dictionary)
            {
                builderInsert.Append(string.Format("{0}, ", item.Key));
                builderValues.Append(string.Format("@{0}, ", item.Key));
            }

            builderInsert.Append(")");
            builderValues.Append(")");

            builderInsert.Replace(",", "", builderInsert.ToString().Length - 3, 1);
            builderValues.Replace(",", "", builderValues.ToString().Length - 3, 1);

            builderInsert.Append(string.Format(" {0}", builderValues.ToString()));

            query = builderInsert.ToString();

            return query;
        }

        private string MakeUpdateQuery(Dictionary<string, object> dictionary)
        {
            string query = "";
            StringBuilder builderUpdate = new StringBuilder();
            StringBuilder builderWhere = new StringBuilder();

            string tableName = NamingService.GetTableNameFromDomainModel(typeof(T));

            builderUpdate.Append(string.Format("UPDATE {0} SET ", tableName));
            builderWhere.Append(string.Format("WHERE "));

            foreach (var item in dictionary)
            {
                if (!item.Key.Contains("Id"))
                {
                    builderUpdate.Append(string.Format("{0} = @{0}, ", item.Key));
                }
                else
                {
                    builderWhere.Append(string.Format("{0} = @{0} ", item.Key));
                }
            }

            builderUpdate.Replace(",", "", builderUpdate.ToString().Length - 2, 1);
            builderUpdate.Append(string.Format("{0}", builderWhere.ToString()));

            query = builderUpdate.ToString();

            return query;
        }
    }
}
