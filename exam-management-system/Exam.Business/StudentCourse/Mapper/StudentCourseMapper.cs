using System;

namespace Exam.Business.StudentCourse
{
    public class StudentCourseMapper : IStudentCourseMapper
    {
        public Domain.Entities.StudentCourse Map(Guid studentId, StudentCourseCreationDto studentCourseCreationDto)
        {
            return new Domain.Entities.StudentCourse(studentId, studentCourseCreationDto.CourseId);
        }

        public StudentCourseDetailsDto Map(Domain.Entities.StudentCourse studentCourse)
        {
            StudentCourseDetailsDto studentCourseDetailsDto = new StudentCourseDetailsDto
            {
                StudentId = studentCourse.StudentId,
                Student = studentCourse.Student,
                CourseId = studentCourse.CourseId,
                Course = studentCourse.Course
            };
            return studentCourseDetailsDto;
        }
    }
}
