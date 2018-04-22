using EducationPortal.Models;
using System.Web.Mvc;

namespace EducationPortal.Attributes
{
    public class AdminOnly : ActionFilterAttribute
    {
        private string redirectController = "";
        private string redirectAction = "";

        public AdminOnly(string controller = "home", string action = "")
        {
            redirectController = controller;
            redirectAction = action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!Authorization.IsAdmin)
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.RequestContext.HttpContext
                    .Response
                    .Redirect(urlHelper.Action(redirectAction, redirectController));
            } else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}