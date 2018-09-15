using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Category;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Category
{
    public class CategoryDomainModelBuilder : DomainModelBuilder<CategoryData, CategoryViewModel>
    {
        public CategoryDomainModelBuilder(CategoryViewModel viewModel) : base(viewModel)
        {
            model = new CategoryData();
        }

        public override void BuildConcreteData()
        {
            model.CodeCategory = viewModel.Code;
            model.NameCategory = viewModel.Name;
        }
    }
}