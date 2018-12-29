using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exam.Business.Course;

namespace Exam.Business.StudentCourse.Service
{
    public interface IStudentCourseService
    {
        Task<StudentCourseDetailsDto> AddCourse(Guid id, StudentCourseCreationDto studentCourseCreationDto);

        Task<List<CourseDto>> GetCourses(Guid id);
    }
}
