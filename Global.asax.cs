using EducationPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EducationPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var db = new Models.EducationPortalContainer();
            if(!db.Accounts.Any(m => m is Models.Admin))
            {
                db.Accounts.Add(new Models.Admin()
                {
                    Login = "admin",
                    Password = "admin".EncryptMD5(),
                    CreationTime = DateTime.Now
                });

                db.SaveChanges();
            }
            
            if (db.Images.Count() == 0)
            {
                db.Images.Add(new Models.Image()
                {
                    FileName = "blank-image.jpg",
                    LastModified = DateTime.Now
                });
                
                db.SaveChanges();
            }

        }
    }
}
