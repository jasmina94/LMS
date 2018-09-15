using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Language;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Language
{
    public class LanguageViewModelBuilder : ViewModelBuilder<LanguageViewModel, LanguageData>
    {
        public LanguageViewModelBuilder(LanguageData model) : base(model)
        {
            viewModel = new LanguageViewModel();
        }

        public override void BuildViewModelConcreteData()
        {
            viewModel.Code = model.CodeLanguage;
            viewModel.Name = model.NameLanguage;
        }
    }
}