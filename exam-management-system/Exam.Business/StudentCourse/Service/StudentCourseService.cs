using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Exam.Business.Student.Exception;
using Exam.Business.StudentCourse.Exception;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.StudentCourse.Service
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;
        private readonly IStudentCourseMapper studentCourseMapper;
        private readonly ICourseMapper courseMapper;

        public StudentCourseService(IReadRepository readRepository, IWriteRepository writeRepository, IStudentCourseMapper studentCourseMapper, ICourseMapper courseMapper)
        {
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.studentCourseMapper = studentCourseMapper ?? throw new ArgumentNullException();
            this.courseMapper = courseMapper ?? throw new ArgumentNullException();
        }

        public async Task<StudentCourseDetailsDto> AddCourse(Guid id, StudentCourseCreationDto studentCourseCreationDto)
        {
            var student = await this.readRepository.GetByIdAsync<Domain.Entities.Student>(id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            var course = await this.readRepository.GetByIdAsync<Domain.Entities.Course>(studentCourseCreationDto.CourseId);
            if (course == null)
            {
                throw new CourseNotFoundException(studentCourseCreationDto.CourseId);
            }

            if (student.YearOfStudy < course.Year)
            {
                throw new StudentCannotApplyException(id, studentCourseCreationDto.CourseId);
            }

            var appliedCourses = await this.GetCourses(id);
            if (appliedCourses.Any(c => c.Id == course.Id))
            {
                throw new StudentAlreadyAppliedException(id, studentCourseCreationDto.CourseId);
            }

            var studentCourse = this.studentCourseMapper.Map(id, studentCourseCreationDto);
            await this.writeRepository.AddNewAsync(studentCourse);
            await this.writeRepository.SaveAsync();
            return this.studentCourseMapper.Map(studentCourse);
        }

        public async Task<List<CourseDto>> GetCourses(Guid id)
        {
            List<CourseDto> courses = new List<CourseDto>();

            var student = await this.readRepository.GetAll<Domain.Entities.Student>().Where(s => s.Id == id)
                .Include(s => s.StudentCourses).ThenInclude(sc => sc.Course).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            foreach (var studentCourse in student.StudentCourses)
            {
                courses.Add(this.courseMapper.Map(studentCourse.Course));
            }

            return courses;
        }
    }
}
