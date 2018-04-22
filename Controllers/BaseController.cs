using EducationPortal.Models;
using System.Web.Mvc;

namespace EducationPortal.Controllers
{
    public abstract class BaseController : Controller
    {
        public EducationPortalContainer db = new EducationPortalContainer();
    }
}