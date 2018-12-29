using System;

namespace Exam.Business.StudentCourse
{
    public interface IStudentCourseMapper
    {
        Domain.Entities.StudentCourse Map(Guid studentId, StudentCourseCreationDto studentCourseCreationDto);

        StudentCourseDetailsDto Map(Domain.Entities.StudentCourse studentCourse);
    }
}
