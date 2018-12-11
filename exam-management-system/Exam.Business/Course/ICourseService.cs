using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Business.Course
{
    public interface ICourseService
    {
        Domain.Entities.Course Create();
        IEnumerable<CourseDto> GetAll();
        CourseDto GetById(Guid id);
        Task<CourseDto> Update(Guid existingCourseId, CourseCreatingDto courseCreatingDto);

        Task Delete(Guid existingCourseId);
    }
}
