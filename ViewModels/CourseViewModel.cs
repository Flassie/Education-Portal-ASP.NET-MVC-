using EducationPortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationPortal.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<LessonViewModel> lessons;

        public CourseViewModel() : this(null) { }
        public CourseViewModel(IEnumerable<LessonViewModel> lessons = null)
        {
            this.lessons = lessons;
        }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Required]
        [RegularExpression("\\d+[\\.|\\,]{0,1}\\d{0,}")]
        public string Price { get; set; }

        public static implicit operator Course(CourseViewModel viewModel)
        {
            return new Course
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                About = viewModel.About,
                Price = double.Parse(viewModel.Price.Replace(".", ","))
            };
        }
    }
}