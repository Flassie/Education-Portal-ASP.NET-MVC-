using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.ViewModels
{
    public class LessonsViewModel
    {
        public LessonsViewModel(IEnumerable<CourseViewModel> courses)
        {
            this.Courses = courses;
        }
        
        public IEnumerable<CourseViewModel> Courses { get; private set; }

        public LessonViewModel NewLessonViewModel = new LessonViewModel();
    }
}