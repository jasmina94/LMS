using LMS.BusinessLogic.AccessControlManagement.Interfaces;
using LMS.Infrastructure.ActionFilters;
using LMS.Infrastructure.Authorization.Constants;
using LMS.Models.ViewModels.Account;
using System.Web.Mvc;
using System.Web.Security;

namespace LMS.Controllers
{
    public class AccountController : Controller
    {
        #region Injected properties

        public IAccessControlService AccessControlService { get; set; }

        #endregion

        public ActionResult Index()
        {
            ActionResult result = null;
            int loggedUserId = AccessControlService.CheckCookie(Request);

            if (Session[SessionConstant.USER] != null)
            {
                result = RedirectToAction("Index", "Home");
            }
            else if (loggedUserId != 0)
            {
                AccessControlService.SetCurrentUser(Session, loggedUserId);
                result = RedirectToAction("Index", "Home");
            }
            else
            {
                result = View(new LoginViewModel());
            }

            return result;
        }

        [HttpPost]
        [ValidateModelFilter]
        public JsonResult Login(LoginViewModel loginViewModel)
        {
            JsonResult result = (JsonResult)RouteData.Values["validation"];
            ValidationResponse validationResponse = (ValidationResponse)result.Data;

            if (validationResponse.Success)
            {
                if (Membership.ValidateUser(loginViewModel.Username, loginViewModel.Password))
                {
                    AccessControlService.SetCurrentUser(Session, Response, loginViewModel);
                    validationResponse.RedirectionUrl = PathConstants.HOME_PATH;
                }
                else
                {
                    validationResponse.Message = "Unknow user with this credentials. Try again!";
                    validationResponse.Success = false;
                }
            }

            return result;
        }
    }
}