using LMS.DomainModel.Infrastructure.ORM.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace LMS.DomainModel.Infrastructure.ORM.Mapper.Interfaces
{
    public interface ILMSMapper
    {
        Dictionary<Type, IMappingModel> GetMappingCollection();

        IMappingModel GetMappingModelForType(Type modelType);
    }
}
