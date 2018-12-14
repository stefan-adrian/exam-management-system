using System;
using Exam.Business.Course;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class CourseTestUtils
    {
        public static Course GetCourse()
        {
            return new Course("course", 1);
        }

        public static CourseDto GetCourseDetailsDto(Guid id)
        {
            return new CourseDto(id, "course", 1);
        }

        public static CourseCreatingDto GetCourseCreatingDto()
        {
            return new CourseCreatingDto
            {
                Name = "course",
                Year = 1
            };
        }
    }
}
