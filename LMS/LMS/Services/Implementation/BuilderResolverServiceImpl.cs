using Autofac;
using LMS.Services.Interfaces;
using System;

namespace LMS.Services.Implementation
{
    public class BuilderResolverServiceImpl : IBuilderResolverService
    {
        public IComponentContext ComponetContext { get; set; }

        public TBuilder Get<TBuilder, TModel>(TModel model)
        {
            var parameter = new TypedParameter(typeof(TModel), model);
            TBuilder builder = ComponetContext.Resolve<TBuilder>(parameter);

            return builder;
        }
    }
}