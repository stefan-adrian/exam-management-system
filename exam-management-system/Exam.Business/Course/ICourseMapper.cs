namespace Exam.Business.Course
{
    public interface ICourseMapper
    {
        Domain.Entities.Course Map(CourseDto courseDto);

        CourseDto Map(Domain.Entities.Course course);

    }
}