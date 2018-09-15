using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Autofac;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;
using LMS.DomainModel.Infrastructure.ORM.Model.Interfaces;
using LMS.DomainModel.Infrastructure.ORM.Mapper.Interfaces;
using LMS.DomainModel.Infrastructure.ORM;

namespace LMS.DomainModel.Repository.Base.Implementation
{
    public abstract class Repository<T> : BaseRepository<T>, IStartable, IRepository<T> where T : class
    {
        protected IMappingModel mappingModel;
        public ILMSMapper LMSMapper { get; set; }

        public Repository() : base()
        {
        }

        public List<T> GetAllData()
        {
            SqlCommand sqlCommand = CreateSqlCommandGetAll();
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);
            List<T> dataRows = ConvertToList(dataRowCollection);

            return dataRows;
        }

        public List<T> GetAllDataInTransaction(SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandGetAll();
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollectionInTransaction(sqlCommand, sqlTransaction);
            List<T> dataRows = ConvertToList(dataRowCollection);

            return dataRows;
        }

        public List<T> GetAllActiveData()
        {
            SqlCommand sqlCommand = CreateSqlCommandGetAllActive();
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);
            List<T> dataRows = ConvertToList(dataRowCollection);

            return dataRows;
        }

        public List<T> GetAllActiveDataInTransaction(SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandGetAllActive();
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollectionInTransaction(sqlCommand, sqlTransaction);
            List<T> dataRows = ConvertToList(dataRowCollection);

            return dataRows;
        }

        public T GetDataById(int id)
        {
            SqlCommand sqlCommand = CreateSqlCommandGetById(id);
            DataRow dataRow = DataSource.GetDataRow(sqlCommand);
            T data = null;

            if (dataRow != null)
            {
                var mapping = (MappingModel<T>)mappingModel;
                data = mapping.ExecuteConversion(dataRow);
            }

            return data;
        }

        public T GetDataByIdInTransaction(int id, SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandGetById(id);
            DataRow dataRow = DataSource.GetDataRowInTransaction(sqlCommand, sqlTransaction);
            T data = null;

            if (dataRow != null)
            {
                var mapping = (MappingModel<T>)mappingModel;
                data = mapping.ExecuteConversion(dataRow);
            }

            return data;
        }

        public void DeleteAllData()
        {
            SqlCommand sqlCommand = CreateSqlCommandDeleteAllData();

            DataSource.ExecuteNonQuery(sqlCommand);
        }

        public void DeleteAllDataInTransaction(SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandDeleteAllData();

            DataSource.ExecuteNonQueryInTransaction(sqlCommand, sqlTransaction);
        }

        public void DeleteById(int id)
        {
            SqlCommand sqlCommand = CreateSqlCommandDeleteById(id);

            DataSource.ExecuteNonQuery(sqlCommand);
        }

        public void DeleteByIdInTransaction(int id, SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandDeleteById(id);

            DataSource.ExecuteNonQueryInTransaction(sqlCommand, sqlTransaction);
        }

        public List<T> ConvertToList(DataRowCollection dataRowCollection)
        {
            List<T> listOfData = new List<T>();

            if (dataRowCollection != null)
            {
                foreach (DataRow dataRow in dataRowCollection)
                {
                    var mapping = (MappingModel<T>)mappingModel;
                    T item = mapping.ExecuteConversion(dataRow);

                    listOfData.Add(item);
                }
            }

            return listOfData;
        }        

        public int SaveData(BaseData data)
        {
            if (data.Id == 0)
            {
                data.Id = GetNextId();
                InsertData(data);
            }
            else
            {
                UpdateData(data);
            }

            return data.Id;
        }

        public int SaveDataInTransaction(BaseData data, SqlTransaction sqlTransaction)
        {
            if (data.Id == 0)
            {
                data.Id = GetNextId();
                InsertDataInTransaction(data, sqlTransaction);
            }
            else
            {
                UpdateDataInTransaction(data, sqlTransaction);
            }

            return data.Id;
        }

        private void InsertData(BaseData data)
        {
            SqlCommand sqlCommand;
            var mapping = (MappingModel<T>)mappingModel;

            mapping.Model = data;
            sqlCommand = mapping.ExecuteCommand(ORMConstant.Insert);

            DataSource.ExecuteNonQuery(sqlCommand);
        }

        private void InsertDataInTransaction(BaseData data, SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand;
            var mapping = (MappingModel<T>)mappingModel;

            mapping.Model = data;
            sqlCommand = mapping.ExecuteCommand(ORMConstant.Insert);

            DataSource.ExecuteNonQueryInTransaction(sqlCommand, sqlTransaction);
        }

        private void UpdateData(BaseData data)
        {
            SqlCommand sqlCommand;
            var mapping = (MappingModel<T>)mappingModel;

            mapping.Model = data;
            sqlCommand = mapping.ExecuteCommand(ORMConstant.Update);

            DataSource.ExecuteNonQuery(sqlCommand);
        }

        private void UpdateDataInTransaction(BaseData data, SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand;
            var mapping = (MappingModel<T>)mappingModel;

            mapping.Model = data;
            sqlCommand = mapping.ExecuteCommand(ORMConstant.Update);

            DataSource.ExecuteNonQueryInTransaction(sqlCommand, sqlTransaction);
        }

        public void Start()
        {
            Type modelType = typeof(T);
            mappingModel = LMSMapper.GetMappingModelForType(modelType);
        }
    }
}
