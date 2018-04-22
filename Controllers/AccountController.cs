using EducationPortal.Attributes;
using EducationPortal.Models;
using EducationPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EducationPortal.Controllers
{
    public class AccountController : BaseController
    {
        [UserOnly]
        public ActionResult Index()
        {
            var login = Models.Authorization.Login;
            var user = db.Accounts.FirstOrDefault(m => m.Login == login) as User;
            var history = HistoryViewModel.CreateHistory(db, DateTime.Now.AddDays(-7), DateTime.Now, user);

            IEnumerable<int> watchedLessons = user.FinishedLessons.Select(m => m.Id);

            var courses = db.Courses.ToList().Select(
                m => m.ToViewModel(
                    m.Lessons.Select(
                        lesson => lesson.ToViewModel(watchedLessons.Any(watchedId => watchedId == lesson.Id )))));
            //                    ^^^^^^^^^^^^^^^^^| ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            //                          (1)                                       (2)
            // (1): lesson.ToViewModel(bool isWatched). Creating ViewModel from Enttiy model using extension. Marks it watched/unwatched
            // (2): Looking to watchedLessons and returns true if we've found current LessonId

            var model = new AccountViewModel()
            {
                Account = user,
                History = history,
                Courses = courses
            };

            return View(model);
        }

        [UserOnly]
        public ActionResult Course(int? id)
        {
            var login = Models.Authorization.Login;
            var user = db.Accounts.FirstOrDefault(m => m.Login == login) as User;

            var course = db.Courses.FirstOrDefault(m => m.Id == id);
            if(course == null)
            {
                ViewBag.IsError = true;
                ViewBag.ErrorMsg = "Course not found";

                return View();
            }

            var lessons = course.Lessons.Select(m => m.ToViewModel()).ToList();
            IEnumerable<int> watchedLessons = user.FinishedLessons.Select(m => m.Id);
            for(int i = 0; i < lessons.Count(); i++)
            {
                lessons[i].IsWatched = watchedLessons.Any(wi => wi == lessons[i].Id);
            }

            return View(course.ToViewModel(lessons));
        }

        [UserOnly]
        public ActionResult MarkWatched(int id, bool isWatched)
        {
            var login = Models.Authorization.Login;
            var user = db.Accounts.FirstOrDefault(m => m.Login == login) as User;

            var lesson = db.Lessons.FirstOrDefault(m => m.Id == id);
            if(lesson != null)
            {
                if(isWatched)
                {
                    user.FinishedLessons.Add(lesson);
                } else
                {
                    user.FinishedLessons.Remove(lesson);
                }

                db.SaveChanges();
            }
            var prevUrl = Request.UrlReferrer.ToString();
            return Redirect(prevUrl);
        }

        [HttpPost]
        public ActionResult Login(HomeViewModel.SignInViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            var encryptedPassword = model.Password.EncryptMD5();

            var account = db.Accounts.FirstOrDefault(m => m.Login == model.Login && m.Password == encryptedPassword);
            if(account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Login or password is wrong");
            }

            if(account is Models.Admin)
            {
                Models.Authorization.IsAdmin = true;
            } else if(account is Models.User)
            {
                Models.Authorization.IsUser = true;
            }

            Models.Authorization.Login = account.Login;
            
            return RedirectToAction("index", "home");
        }
        
        public ActionResult Logout()
        {
            Models.Authorization.IsAuthorized = false;
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult Register(HomeViewModel.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong data provided");
            }

            var existingAccount = db.Accounts.FirstOrDefault(m => m.Login == model.Login);
            if(existingAccount != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Account with this login is already exists");
            }

            db.Accounts.Add(new Models.User
            {
                Login = model.Login,
                Password = model.Password.EncryptMD5(),
                CreationTime = DateTime.Now,
                LastActivity = DateTime.Now
            });
            db.SaveChanges();

            var signInModel = new HomeViewModel.SignInViewModel()
            {
                Login = model.Login,
                Password = model.Password
            };

            return Login(signInModel);
        }
    }
}