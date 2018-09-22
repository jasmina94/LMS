using LMS.BusinessLogic.LanguageManagement.Model;
using LMS.Models.ViewModels.Language;
using System.Collections.Generic;

namespace LMS.BusinessLogic.LanguageManagement.Interfaces
{
    public interface ILanguageService
    {
        LanguageViewModel Get(int? languageId);

        List<LanguageViewModel> GetAll(bool active);

        SaveLanguageResult Save(LanguageViewModel viewModel);

        DeleteLanguageResult Delete(int? languageId);
    }
}
