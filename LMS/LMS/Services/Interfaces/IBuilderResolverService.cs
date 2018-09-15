namespace LMS.Services.Interfaces
{
    public interface IBuilderResolverService
    {
        TBuilder Get<TBuilder, TModel>(TModel model);
    }
}
