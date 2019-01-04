using System;

namespace Exam.Business.StudentCourse
{
    public class StudentCourseDetailsDto
    {
        public Guid StudentId { get; set; }

        public Domain.Entities.Student Student { get; set; }

        public Guid CourseId { get; set; }

        public Domain.Entities.Course Course { get; set; }
    }
}
