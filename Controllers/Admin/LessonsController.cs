using EducationPortal.Attributes;
using EducationPortal.Models;
using EducationPortal.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EducationPortal.Controllers.Admin
{
    [RoutePrefix("admin/lessons"), Route("{action=index}")]
    [AdminOnly]
    public class LessonsController : BaseController
    {
        public ActionResult Index(int? courseId)
        {
            var courses = (courseId == null ? db.Courses : db.Courses.Where(course => course.Id == courseId)).ToList();

            var model = new LessonsViewModel(
                courses.Select(course => course.ToViewModel(
                    course.Lessons.Select(lesson => lesson.ToViewModel()
                    ))));

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(LessonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            Lesson newLesson = model;
            newLesson.CreationTime = DateTime.Now;
            db.Lessons.Add(newLesson);
            db.SaveChanges();

            return PartialView("_LessonCard", newLesson.ToViewModel());
        }

        [HttpPost]
        public ActionResult Update(LessonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            Lesson updatedLesson = model;

            var lesson = db.Lessons.FirstOrDefault(m => m.Id == model.Id);
            lesson.Name = updatedLesson.Name;
            lesson.VideoLink = updatedLesson.VideoLink;

            db.SaveChanges();

            return PartialView("_LessonCard", lesson.ToViewModel());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var lesson = db.Lessons.FirstOrDefault(c => c.Id == id);
            if (lesson == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Lesson with id = " + id + " not found");
            }
            else
            {
                foreach(var account in db.Accounts)
                {
                    if(account is User)
                    {
                        var user = account as User;
                        user.FinishedLessons.Remove(lesson);
                    }
                }

                db.Lessons.Remove(lesson);
                db.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }
    }
}