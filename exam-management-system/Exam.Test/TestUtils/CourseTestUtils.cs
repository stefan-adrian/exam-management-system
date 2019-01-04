using System;
using Exam.Business.Course;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class CourseTestUtils
    {
        private static Course course1 = null;
        private static Course course2 = null;

        public static Course GetCourse()
        {
            if (course1 == null)
            {
                course1 = new Course("course", 1, ProfessorTestUtils.GetProfessor());
            }

            return course1;
        }

        public static Course GetCourse2()
        {
            if (course2 == null)
            {
                course2 = new Course("course", 1,ProfessorTestUtils.GetProfessor());
            }

            return course2;
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
