using System;
using System.Threading.Tasks;

namespace Exam.Business.StudentCourse.Service
{
    public interface IStudentCourseService
    {
        Task<StudentCourseDetailsDto> AddCourse(Guid id, StudentCourseCreationDto studentCourseCreationDto);
    }
}
