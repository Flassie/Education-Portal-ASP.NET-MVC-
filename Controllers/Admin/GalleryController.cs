using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EducationPortal.Attributes;
using EducationPortal.Models;

namespace EducationPortal.Controllers
{
    [RoutePrefix("admin/gallery")]
    [Route("{action=index}")]
    [AdminOnly]
    public class GalleryController : BaseController
    {
        public ActionResult Index()
        {
            var images = db.Images.ToList();
            return View(images);
        }
        
        [HttpPost]
        public ActionResult Add()
        {
            if(Request.Files.Count != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Expected 1 image, not " + Request.Files.Count + " images");
            }

            var file = Request.Files[0];
            if(!file.ContentType.ToLower().StartsWith("image/"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Image should be provided");
            }
            
            var extension = Path.GetExtension(file.FileName);
            var name = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var filename = name + "." + extension;

            var resultFilePath = Server.MapPath("~/Content/images/gallery/" + filename);

            file.SaveAs(resultFilePath);

            var image = new Image()
            {
                FileName = filename,
                LastModified = DateTime.Now
            };

            db.Images.Add(image);
            db.SaveChanges();

            return PartialView("_GalleryCard", image);
        }
        
        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var image = db.Images.FirstOrDefault(img => img.Id == id);
            if(image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Image with id " + id + " not found");
            }

            var filepath = Server.MapPath("~/Content/images/gallery/" + image.FileName);
            if(System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            db.Images.Remove(image);
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}