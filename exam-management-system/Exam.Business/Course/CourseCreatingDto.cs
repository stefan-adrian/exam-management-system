using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Course
{
    public class CourseCreatingDto
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public int Year { get; set; }

    }
}
