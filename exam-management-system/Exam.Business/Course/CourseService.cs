using System;
using System.Collections.Generic;
using System.Linq;
using Exam.Domain.Interfaces;

namespace Exam.Business.Course
{
    public class CourseService : ICourseService
    {
        private readonly IReadRepository readRepository;

        private readonly IWriteRepository writeRepository;

        private readonly ICourseMapper courseMapper;

        public CourseService(IReadRepository readRepository, IWriteRepository writeRepository,ICourseMapper courseMapper)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
            this.courseMapper = courseMapper;
        }

        Domain.Entities.Course ICourseService.Create()
        {
            CourseDto courseDto = new CourseDto(Guid.NewGuid(), "Information Security",3);
            
            Domain.Entities.Course course = courseMapper.Map(courseDto);

            writeRepository.AddNewAsync(course);
            writeRepository.SaveAsync();

            return course;
        }

        IEnumerable<CourseDto> ICourseService.GetAll()
        {
            List<CourseDto> courseDtos = new List<CourseDto>();
            IEnumerable<Domain.Entities.Course> courses = readRepository.GetAll<Domain.Entities.Course>().ToList();
            foreach (var course in courses)
            {
                courseDtos.Add(courseMapper.Map(course));
            }

            return courseDtos;
        }
    }
}
