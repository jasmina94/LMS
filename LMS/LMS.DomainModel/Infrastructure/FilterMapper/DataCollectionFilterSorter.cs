using LMS.DomainModel.Infrastructure.FilterMapper.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;

namespace LMS.DomainModel.Infrastructure.FilterMapper
{
    public class DataCollectionFilterSorter<TData>
    {
        public IEnumerable<TData> FilterAndSort(IEnumerable<TData> collection, FilterSorterModel filterSorterModel)
        {
            IEnumerable<TData> resultCollection = collection;

            if (filterSorterModel != null)
            {
                resultCollection = Filter(collection, filterSorterModel.Filter);
                resultCollection = Sort(resultCollection, filterSorterModel.Sorter);
            }

            return resultCollection;
        }

        public IEnumerable<TData> Filter(IEnumerable<TData> collection, IEnumerable<FilterModel> filterModel)
        {
            IEnumerable<TData> resultCollection = collection;

            // Filter is setted
            if (filterModel != null)
            {
                try
                {
                    Func<TData, bool> filterPredicate = GenerateFilterPredicate(filterModel);

                    if (filterPredicate != null)
                    {
                        resultCollection = collection.Where<TData>(filterPredicate);
                    }
                }
                catch (Exception)
                {
                    resultCollection = new List<TData>();
                }
            }

            return resultCollection;
        }

        public IEnumerable<TData> Sort(IEnumerable<TData> collection, SorterModel sorterModel)
        {
            IEnumerable<TData> resultCollection = collection;

            // Sorter is setted correctrly
            if (IsValidSorterModel(sorterModel))
            {
                string orderByExpression = GenerateOrderByExpression(sorterModel);

                resultCollection = collection.OrderBy(orderByExpression).ToList();
            }

            return resultCollection;
        }

        private Func<TData, bool> GenerateFilterPredicate(IEnumerable<FilterModel> filterModel)
        {
            Func<TData, bool> filterPredicate = null;
            ParameterExpression inputParameter = Expression.Parameter(typeof(TData), "inputParameter");
            BinaryExpression conditionBody = GenerateConditionBody(filterModel, inputParameter);

            if (conditionBody != null)
            {
                Expression<Func<TData, bool>> filterPredicateLambda = Expression.Lambda<Func<TData, bool>>(conditionBody, new[] { inputParameter });

                filterPredicate = filterPredicateLambda.Compile();
            }

            return filterPredicate;
        }

        private BinaryExpression GenerateConditionBody(IEnumerable<FilterModel> filterModel, ParameterExpression inputParameter)
        {
            BinaryExpression conditionBody = null;

            foreach (FilterModel filterModelItem in filterModel)
            {
                BinaryExpression conditionBodyPart = GenerateConditionBodyPart(filterModelItem, inputParameter);

                if (conditionBodyPart != null)
                {
                    if (conditionBody == null)
                    {
                        conditionBody = conditionBodyPart;
                    }
                    else
                    {
                        conditionBody = Expression.AndAlso(conditionBody, conditionBodyPart);
                    }
                }
            }

            return conditionBody;
        }

        private BinaryExpression GenerateConditionBodyPart(FilterModel filterModelItem, ParameterExpression inputParameter)
        {
            BinaryExpression conditionBodyPart = null;

            if (filterModelItem.Value != null && filterModelItem.Value.Trim() != "")
            {
                MemberExpression conditionBodyPartProperty = Expression.Property(inputParameter, filterModelItem.Name);
                ConstantExpression conditionBodyPartConstant = GenerateConditionBodyPartConstant(filterModelItem);

                if (filterModelItem.Type == "date")
                {
                    conditionBodyPartProperty = Expression.Property(conditionBodyPartProperty, "Date");
                }

                conditionBodyPart = Expression.Equal(conditionBodyPartProperty, conditionBodyPartConstant);
            }

            return conditionBodyPart;
        }

        private ConstantExpression GenerateConditionBodyPartConstant(FilterModel filterModelItem)
        {
            String filterModelItemValue = filterModelItem.Value.Trim();
            object filterModelItemValueObject = null;

            switch (filterModelItem.Type)
            {
                case "int":
                    filterModelItemValueObject = Int32.Parse(filterModelItemValue);
                    break;
                case "string":
                    filterModelItemValueObject = filterModelItemValue;
                    break;
                case "date":
                    filterModelItemValueObject = DateTime.ParseExact(filterModelItemValue, filterModelItem.Format, CultureInfo.InvariantCulture).Date;
                    break;
                case "time":
                    filterModelItemValueObject = TimeSpan.Parse(filterModelItemValue);
                    break;
                default:
                    filterModelItemValueObject = filterModelItemValue;
                    break;
            }

            return Expression.Constant(filterModelItemValueObject);
        }

        private bool IsValidSorterModel(SorterModel sorterModel)
        {
            return (sorterModel != null && (sorterModel.Order == "asc" || sorterModel.Order == "desc"));
        }

        private string GenerateOrderByExpression(SorterModel sorterModel)
        {
            StringBuilder buildOrderByExpression = new StringBuilder();

            buildOrderByExpression.Append(sorterModel.Name)
               .Append(" ")
               .Append(sorterModel.Order.ToUpper());

            return buildOrderByExpression.ToString();
        }
    }
}
