using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EducationPortal.ViewModels
{
    public static class Extensions
    {
        public static CourseViewModel ToViewModel(this Course course, IEnumerable<LessonViewModel> lessons = null)
        {
            var lessonsList = lessons == null ? course.Lessons.ToViewModelList() : lessons;

            return new CourseViewModel(lessons)
            {
                Id = course.Id,
                Name = course.Name,
                About = course.About,
                Price = course.Price.ToString(),
                lessons = lessonsList
            };
        }

        public static IEnumerable<LessonViewModel> ToViewModelList(this IEnumerable<Lesson> lessons, bool isWatched = false)
        {
            return lessons.Select(m => m.ToViewModel()).ToList();
        }

        public static LessonViewModel ToViewModel(this Lesson lesson, bool isWatched = false)
        {
            return new LessonViewModel()
            {
                Id = lesson.Id,
                Course = lesson.CourseId,
                Name = lesson.Name,
                VideoLink = lesson.VideoLink,
                IsWatched = isWatched
            };
        }

        // For Passwords
        public static string EncryptMD5(this string pass)    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5; 
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }

        // To simplify navbar creating
        public static MvcHtmlString NavbarLink(this HtmlHelper html, string name, 
            string controller = "", string action = "", string activeCssClass = "active")
        {
            var context = html.ViewContext;
            if (context.Controller.ControllerContext.IsChildAction)
                context = html.ViewContext.ParentActionViewContext;

            var routeValues = context.RouteData.Values;
            string currentAction = routeValues["action"].ToString().ToLower();
            string currentController = routeValues["controller"].ToString().ToLower();

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            if (String.IsNullOrEmpty(controller))
                controller = name.ToLower();

            var cssClass = (currentAction == action && controller == currentController) 
                ? activeCssClass : String.Empty;

            return html.ActionLink(name, action, controller, null, new { @class = "nav-item nav-link " + cssClass });
        }
    }
}