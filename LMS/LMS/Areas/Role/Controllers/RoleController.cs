using LMS.BusinessLogic.RoleManagement.Interfaces;
using LMS.Infrastructure.Extension;
using LMS.Models.ViewModels.Role;
using LMS.MVC.Infrastructure.SelectHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Areas.Role.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService RoleService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSelect(string searchTerm, int pageSize, int pageNum)
        {
            IQueryable<RoleViewModel> queryRoleList = RoleService.GetAll(true).AsQueryable();
            var roles = new List<RoleViewModel>();
            int roleCount = 0;

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            queryRoleList = queryRoleList.Where(x => x.Name.Like(searchTerm));
            roleCount = queryRoleList.Count();

            roles = queryRoleList.Skip(pageSize * (pageNum - 1))
                               .Take(pageSize)
                               .ToList();

            SelectPageResult pagedUsers = SelectGenerator.RolesToSelectPageResult(roles, roleCount);

            return new JsonResult
            {
                Data = pagedUsers,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}