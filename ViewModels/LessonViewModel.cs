using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationPortal.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }

        public bool IsWatched { get; set; }

        [Required]
        public int Course { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("https:\\/\\/www\\.youtube\\.com\\/((watch\\?v=.+)|(embed\\/.+))")]
        public string VideoLink { get; set; }

        public static implicit operator Lesson(LessonViewModel viewModel)
        {
            var link = viewModel.VideoLink;
            if (link.Contains("watch?v="))
            {
                link = link.Replace("watch?v=", "embed/");
                var queryStartIndex = link.IndexOf("&");
                if (queryStartIndex > 0)
                {
                    link = link.Substring(0, queryStartIndex);
                }
            }

            return new Lesson
            {
                Id = viewModel.Id,
                CourseId = viewModel.Course,
                Name = viewModel.Name,
                VideoLink = link
            };
        }
    }
}