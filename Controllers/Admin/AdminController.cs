using System.Web.Mvc;
using EducationPortal.Attributes;

namespace EducationPortal.Controllers
{
    [AdminOnly]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}