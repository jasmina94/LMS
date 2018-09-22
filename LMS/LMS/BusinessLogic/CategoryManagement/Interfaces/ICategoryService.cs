using LMS.BusinessLogic.CategoryManagement.Model;
using LMS.Models.ViewModels.Category;
using System.Collections.Generic;

namespace LMS.BusinessLogic.CategoryManagement.Interfaces
{
    public interface ICategoryService
    {
        CategoryViewModel Get(int? categoryId);

        List<CategoryViewModel> GetAll(bool active);

        SaveCategoryResult Save(CategoryViewModel viewModel);

        DeleteCategoryResult Delete(int? categoryId);
    }
}
