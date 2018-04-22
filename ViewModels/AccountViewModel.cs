using System.Collections.Generic;

namespace EducationPortal.ViewModels
{
    public class AccountViewModel
    {
        public HistoryViewModel History { get; set; }
        public Models.User Account { get; set; }

        public IEnumerable<CourseViewModel> AccountCourses { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IEnumerable<LessonViewModel> Lessons { get; set; }
        public IEnumerable<int> WatchedLessons { get; set; }
    }
}