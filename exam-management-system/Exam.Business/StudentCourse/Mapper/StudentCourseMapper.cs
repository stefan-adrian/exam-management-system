using System;

namespace Exam.Business.StudentCourse
{
    public class StudentCourseMapper : IStudentCourseMapper
    {
        public Domain.Entities.StudentCourse Map(Guid studentId, StudentCourseCreationDto studentCourseCreationDto)
        {
            return new Domain.Entities.StudentCourse(studentId, studentCourseCreationDto.CourseId);
        }
    }
}
