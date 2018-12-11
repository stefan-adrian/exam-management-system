using System;
namespace Exam.Business.Course
{
    public interface ICourseMapper
    {
        Domain.Entities.Course Map(CourseDto courseDto);

        CourseDto Map(Domain.Entities.Course course);

        Domain.Entities.Course Map(CourseCreatingDto courseCreatingDto);

        Domain.Entities.Course Map(CourseDto courseDetails, Domain.Entities.Course course);

        CourseDto Map(Guid courseId, CourseCreatingDto courseCreatingDto);
    }
}