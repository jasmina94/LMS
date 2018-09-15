using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Category;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Category
{
    public class CategoryViewModelBuilder : ViewModelBuilder<CategoryViewModel, CategoryData>
    {
        public CategoryViewModelBuilder(CategoryData model) : base(model)
        {
            viewModel = new CategoryViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.Code = model.CodeCategory;
            viewModel.Name = model.NameCategory;
        }
    }
}