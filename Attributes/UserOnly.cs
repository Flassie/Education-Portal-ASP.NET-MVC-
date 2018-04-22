using EducationPortal.Models;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace EducationPortal.Attributes
{
    public class UserOnly : ActionFilterAttribute
    {
        private string redirectController = "";
        private string redirectAction = "";

        public UserOnly(string controller = "home", string action = "")
        {
            redirectController = controller;
            redirectAction = action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Authorization.IsUser)
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", redirectController },
                        { "Action", redirectAction }
                    });
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}