using LMS.Services.Interfaces;
using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.Models.ViewModels.Category;
using LMS.Infrastructure.Authorization;
using LMS.DomainModel.Repository.Book.Interfaces;
using LMS.BusinessLogic.CategoryManagement.Model;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.BusinessLogic.CategoryManagement.Interfaces;
using LMS.Infrastructure.ModelBuilders.Implementation.Category;

namespace LMS.BusinessLogic.CategoryManagement.Implementation
{
    public class CategoryServiceImpl : ICategoryService
    {
        #region Injected properties
        public ICategoryRepository CategoryRepository { get; set; }

        public IBookRepository BookRepository { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }

        #endregion

        public CategoryViewModel Get(int? categoryId)
        {
            var viewModel = new CategoryViewModel();
            if (categoryId.HasValue)
            {
                CategoryData domainModel = CategoryRepository.GetDataById(categoryId.Value);
                CategoryViewModelBuilder builder = BuilderResolverService.Get<CategoryViewModelBuilder, CategoryData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<CategoryViewModel> GetAll(bool active)
        {
            var viewModels = new List<CategoryViewModel>();
            var domainModels = new List<CategoryData>();

            domainModels = active ? CategoryRepository.GetAllActiveData() : CategoryRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        private List<CategoryViewModel> ConvertDataToViewModels(List<CategoryData> domainModels)
        {
            var viewModels = new List<CategoryViewModel>();

            foreach (var item in domainModels)
            {
                CategoryViewModelBuilder builder = BuilderResolverService.Get<CategoryViewModelBuilder, CategoryData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }

        public SaveCategoryResult Save(CategoryViewModel viewModel, UserSessionObject user)
        {
            var result = new SaveCategoryResult();

            CategoryDomainModelBuilder builder = BuilderResolverService.Get<CategoryDomainModelBuilder, CategoryViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            CategoryData domainModel = builder.GetDataModel();

            if (viewModel.Id == 0)
                domainModel.RefUserCreatedBy = user.UserId;            

            int id = CategoryRepository.SaveData(domainModel);
            if(id != 0)
                result = new SaveCategoryResult(id, domainModel.NameCategory);

            return result;
        }

        public DeleteCategoryResult Delete(int? categoryId, UserSessionObject user)
        {
            var result = new DeleteCategoryResult();
            if (categoryId.HasValue)
            {
                if (!CheckReferencingBooks(categoryId.Value))
                {
                    CategoryData domainModel = CategoryRepository.GetDataById(categoryId.Value);
                    if (domainModel != null)
                    {
                        CategoryRepository.DeleteById(categoryId.Value, user.UserId);
                        result = new DeleteCategoryResult(categoryId.Value, domainModel.NameCategory);
                    }
                }
                else
                {
                    result.Message = "This category can't be deleted. There are books connected to this category.";
                }
            }

            return result;
        }

        private bool CheckReferencingBooks(int categoryId)
        {
            bool exists = false;
            List<BookData> books = BookRepository.GetBooksByCategory(categoryId);
            if(books != null && books.Count != 0)
            {
                exists = true;
            }

            return exists;
        }
    }
}