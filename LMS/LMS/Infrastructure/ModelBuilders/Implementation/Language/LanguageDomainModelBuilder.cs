using LMS.DomainModel.DomainObject;
using LMS.Infrastructure.ModelBuilders.Abstraction;
using LMS.Models.ViewModels.Language;

namespace LMS.Infrastructure.ModelBuilders.Implementation.Language
{
    public class LanguageDomainModelBuilder : DomainModelBuilder<LanguageData, LanguageViewModel>
    {
        public LanguageDomainModelBuilder(LanguageViewModel viewModel) : base(viewModel)
        {
            model = new LanguageData();
        }

        public override void BuildConcreteData()
        {
            model.CodeLanguage = viewModel.Code;
            model.NameLanguage = viewModel.Name;
        }
    }
}