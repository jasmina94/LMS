using LMS.BusinessLogic.LanguageManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Language;
using System.Collections.Generic;

namespace LMS.BusinessLogic.LanguageManagement.Interfaces
{
    public interface ILanguageService
    {
        LanguageViewModel Get(int? languageId);

        List<LanguageViewModel> GetAll(bool active);

        SaveLanguageResult Save(LanguageViewModel viewModel, UserSessionObject user);

        DeleteLanguageResult Delete(int? languageId, UserSessionObject user);
    }
}
