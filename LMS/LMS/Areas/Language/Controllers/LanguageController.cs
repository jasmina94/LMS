using LMS.BusinessLogic.LanguageManagement.Interfaces;
using LMS.BusinessLogic.LanguageManagement.Model;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Authorization;
using LMS.Infrastructure.Authorization.Attributes;
using LMS.Infrastructure.Extension;
using LMS.Infrastructure.Helpers;
using LMS.Models.ViewModels.Language;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Language.Controllers
{
    public class LanguageController : Controller
    {
        public ILanguageService LanguageService { get; set; }

        [IsAuthenticated]
        public ActionResult Form(int? id)
        {
            var viewModel = LanguageService.Get(id);

            return PartialView("LanguageForm", viewModel);
        }

        [HttpPost]
        [ValidateModelFilter]
        [IsAuthenticated]
        public ActionResult Save(LanguageViewModel viewModel)
        {
            UserSessionObject user = Session.GetUser();
            JsonResult response = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validation = (ValidationResponse)response.Data;

            if (validation.Success)
            {
                SaveLanguageResult result = LanguageService.Save(viewModel, user);
                response.Data = result;
            }

            return response;
        }

        [HttpGet]
        public ActionResult GetSelect(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<LanguageViewModel> queryLanguageList = LanguageService.GetAll(true).AsQueryable();
            var languages = new List<LanguageViewModel>();
            int languagesCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            queryLanguageList = queryLanguageList.Where(x => x.Name.Like(searchTerm));
            languagesCount = queryLanguageList.Count();

            languages = queryLanguageList.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.LanguagesToSelectPageResult(languages, languagesCount);

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
            DeleteLanguageResult deleteResult = LanguageService.Delete(id, user);

            return Json(deleteResult, JsonRequestBehavior.AllowGet);
        }
    }
}