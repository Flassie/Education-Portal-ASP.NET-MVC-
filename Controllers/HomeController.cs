using EducationPortal.Models;
using EducationPortal.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace EducationPortal.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if(Authorization.IsAdmin)
            {
                return RedirectToAction("index", "admin");
            } else if(Authorization.IsUser)
            {
                return RedirectToAction("index", "account");
            }

            var viewModel = new HomeViewModel
            {
                Images = db.Images.ToList(),
                Courses = db.Courses.ToList()
            };

            return View(viewModel);
        }
    }
}