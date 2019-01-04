using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exam.Business.Course;

namespace Exam.Business.StudentCourse.Service
{
    public interface IStudentCourseService
    {
        Task AddCourse(Guid id, StudentCourseCreationDto studentCourseCreationDto);

        Task<List<CourseDto>> GetCourses(Guid id);
    }
}
