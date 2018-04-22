using EducationPortal.Models;
using System.Collections.Generic;
using System.Linq;

namespace EducationPortal.ViewModels
{
    public class CoursesViewModel
    {
        public CoursesViewModel(IEnumerable<Course> courses)
        {
            this.Courses = courses.Select(m => m.ToViewModel(m.Lessons.ToViewModelList()));
        }

        public IEnumerable<CourseViewModel> Courses { get; private set; }
        public CourseViewModel NewCourseViewModel = new CourseViewModel();
    }
}