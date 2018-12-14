using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Course
{
    public class CourseService : ICourseService
    {
        private readonly IReadRepository readRepository;

        private readonly IWriteRepository writeRepository;

        private readonly ICourseMapper courseMapper;

        public CourseService(IReadRepository readRepository, IWriteRepository writeRepository,ICourseMapper courseMapper)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.courseMapper = courseMapper ?? throw new ArgumentNullException();
        }

        public async Task<List<CourseDto>> GetAll()
        {
            return await GetAllCourseDtos().ToListAsync();
        }

        public async Task<CourseDto> GetById(Guid id)
        {
            var course = await this.readRepository.GetByIdAsync<Domain.Entities.Course>(id);
            return this.courseMapper.Map(course);
        }  

        public async Task<CourseDto> Create(CourseCreatingDto newCourse)
        {
            Domain.Entities.Course course = this.courseMapper.Map(newCourse);
            await this.writeRepository.AddNewAsync(course);
            await this.writeRepository.SaveAsync();
            return this.courseMapper.Map(course);
        }

        public async Task<CourseDto> Update(Guid existingCourseId, CourseCreatingDto courseCreatingDto)
        {   CourseDto courseDto = this.courseMapper.Map(existingCourseId, courseCreatingDto);
            var course = this.readRepository.GetByIdAsync<Domain.Entities.Course>(existingCourseId).Result;
            this.writeRepository.Update(this.courseMapper.Map(courseDto, course));
            await this.writeRepository.SaveAsync();
            return courseDto;
        }

        public async Task Delete(Guid existingCourseId)
        {
            var course = this.readRepository.GetByIdAsync<Domain.Entities.Course>(existingCourseId).Result;
            this.writeRepository.Delete(course);
            await this.writeRepository.SaveAsync();
        }

        private IQueryable<CourseDto> GetAllCourseDtos()
        {
            return this.readRepository.GetAll<Domain.Entities.Course>()
                .Select(course => this.courseMapper.Map(course));
        }

    }
}
