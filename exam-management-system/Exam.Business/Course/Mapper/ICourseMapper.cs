using System;
using System.Collections.Generic;

namespace Exam.Business.Course
{
    public interface ICourseMapper
    {
        CourseDto Map(Domain.Entities.Course course);

        Domain.Entities.Course Map(CourseCreatingDto courseCreatingDto);

        Domain.Entities.Course Map(CourseDto courseDetails, Domain.Entities.Course course);

        CourseDto Map(Guid courseId, CourseCreatingDto courseCreatingDto);

        List<CourseDto> Map(List<Domain.Entities.StudentCourse> studentCourses);

        Domain.Entities.Course Map(CourseDto courseDto);
    }
}