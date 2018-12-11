namespace Exam.Business.Course
{
    public class CourseMapper : ICourseMapper
    {
        public Domain.Entities.Course Map(CourseDto courseDto)
        {
            Domain.Entities.Course course = new Domain.Entities.Course(courseDto.Name,courseDto.Year);
            course.SetPropertyValue("Id",courseDto.Id);
            return course;
        }

        public CourseDto Map(Domain.Entities.Course course)
        {
            return new CourseDto(course.Id,course.Name,course.Year);
        }

    }
}
