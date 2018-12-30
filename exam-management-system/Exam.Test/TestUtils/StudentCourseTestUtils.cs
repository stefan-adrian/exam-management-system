using System;
using Exam.Business.StudentCourse;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class StudentCourseTestUtils
    {
        private static StudentCourse studentCourse1 = null;
        private static StudentCourse studentCourse2 = null;

        public static StudentCourse GetStudentCourse()
        {
            if (studentCourse1 == null)
            {
                studentCourse1 = new StudentCourse(Guid.NewGuid(), Guid.NewGuid());
            }

            return studentCourse1;
        }

        public static StudentCourse GetStudentCourse2()
        {
            if (studentCourse2 == null)
            {
                studentCourse2 = new StudentCourse(Guid.NewGuid(), Guid.NewGuid());
            }

            return studentCourse2;
        }

        public static StudentCourseDetailsDto GetStudentCourseDetailsDto(Guid studentId, Guid courseId)
        {
            return new StudentCourseDetailsDto
            {
                StudentId = studentId,
                Student = null,
                CourseId = courseId,
                Course = null
            };
        }

        public static StudentCourseCreationDto GetStudentCourseCreationDto(Guid courseId)
        {
            return new StudentCourseCreationDto
            {
                CourseId = courseId
            };
        }
    }
}
