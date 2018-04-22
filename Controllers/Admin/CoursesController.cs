using EducationPortal.Attributes;
using EducationPortal.Models;
using EducationPortal.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EducationPortal.Controllers
{
    [RoutePrefix("admin/courses")]
    [Route("{action=index}")]
    [AdminOnly]
    public class CoursesController : BaseController
    {
        private DbSet<Course> courses;
        public CoursesController()
        {
            courses = db.Courses;
        }

        public ActionResult Index()
        {
            var viewModel = new CoursesViewModel(courses.ToList());
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            Course newCourse = viewModel;
            newCourse.CreationTime = DateTime.Now;
            courses.Add(newCourse);
            db.SaveChanges();

            return PartialView("_CourseCard", newCourse.ToViewModel());
        }

        [HttpPost]
        public ActionResult Update(CourseViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            Course updatedCourse = model;

            var course = courses.FirstOrDefault(m => m.Id == model.Id);
            course.Name = updatedCourse.Name;
            course.About = updatedCourse.About;
            course.Price = updatedCourse.Price;

            db.SaveChanges();

            return PartialView("_CourseCard", course.ToViewModel());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);
            if(course == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Course with id = " + id + " not found");
            }
            else
            {
                var courseId = course.Id;
                var lessonsToBeRemoved = course.Lessons;
                foreach(var account in db.Accounts)
                {
                    if(account is User)
                    {
                        var user = account as User;
                       
                        foreach(var lesson in lessonsToBeRemoved)
                        {
                            user.FinishedLessons.Remove(lesson);
                        }
                    }
                }

                foreach(var lesson in db.Lessons)
                {
                    db.Lessons.Remove(lesson);
                }
                
                db.Courses.Remove(course);
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }
    }
}