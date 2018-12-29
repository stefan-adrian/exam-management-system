using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.StudentCourse
{
    public class StudentCourseCreationDto
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
