using System;
using AutoMapper;

namespace Exam.Business.Course
{
    public class CourseMapper : ICourseMapper
    {
        private readonly IMapper autoMapper;

        public CourseMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<CourseDto, Domain.Entities.Course>(); }).CreateMapper();
        }

        public CourseDto Map(Domain.Entities.Course course)
        {
            return new CourseDto(course.Id,course.Name,course.Year);
        }

        public CourseDto Map(Guid courseId, CourseCreatingDto courseCreatingDto)
        {
            CourseDto courseDto = new CourseDto(courseId, courseCreatingDto.Name, courseCreatingDto.Year)
            {
                Id = courseId
            };

            return courseDto;
        }

        public Domain.Entities.Course Map(CourseCreatingDto courseCreatingDto)
        {
            Domain.Entities.Course course = new Domain.Entities.Course(courseCreatingDto.Name, courseCreatingDto.Year);
            return course;
        }

        public Domain.Entities.Course Map(CourseDto courseDto, Domain.Entities.Course course)
        {
            this.autoMapper.Map(courseDto, course);
            return course;
        }
    }

}
