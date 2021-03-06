﻿using LMS.BusinessLogic.CategoryManagement.Interfaces;
using LMS.BusinessLogic.CategoryManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Extension;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.Category;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Category.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryService CategoryService { get; set; }

        [IsAuthenticated]
        public ActionResult Form(int? id)
        {
            var viewModel = CategoryService.Get(id);

            return PartialView("CategoryForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        [IsAuthenticated]
        public JsonResult Save(CategoryViewModel viewModel)
        {
            UserSessionObject user = Session.GetUser();
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;
            if (validation.Success)
            {
                SaveCategoryResult result = CategoryService.Save(viewModel, user);
                response.Data = result;
            }

            return response;
        }

        [HttpGet]
        public ActionResult GetSelect(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<CategoryViewModel> queryCategoryList = CategoryService.GetAll(true).AsQueryable();
            var categories = new List<CategoryViewModel>();
            int categoriesCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            queryCategoryList = queryCategoryList.Where(x => x.Name.Like(searchTerm));
            categoriesCount = queryCategoryList.Count();

            categories = queryCategoryList.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.CategoriesToSelectPageResult(categories, categoriesCount);

            return new JsonResult
            {
                Data = pagedUsers,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [IsAuthenticated]
        public ActionResult Delete(int id)
        {
            UserSessionObject user = Session.GetUser();
            DeleteCategoryResult deleteResult = CategoryService.Delete(id, user);

            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }
    }
}