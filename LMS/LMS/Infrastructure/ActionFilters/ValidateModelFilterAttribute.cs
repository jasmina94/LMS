using Autofac;
using LMS.Infrastructure.Validation;
using LMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LMS.Infrastructure.ActionFilters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public IComponentContext ComponetContext { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = "";
            var viewResult = new JsonResult();
            ValidationResponse response = new ValidationResponse();

            IDictionary<string, object> parametersDictionary = filterContext.ActionParameters;
            var modelToValidate = (ViewModel)parametersDictionary.First().Value;

            ILMSValidator validator = ComponetContext.Resolve<ILMSValidator>();
            message = modelToValidate.Validate(validator);
            if (!String.IsNullOrEmpty(message))
            {
                response.Success = false;
                response.Message = "Save operation failed! " + message;
            }
            else
            {
                response.Success = true;
                response.Message = "Save operation finished!";
            }

            viewResult.Data = response;

            filterContext.RouteData.Values.Add("validation", viewResult);
            base.OnActionExecuting(filterContext);
        }
    }
}