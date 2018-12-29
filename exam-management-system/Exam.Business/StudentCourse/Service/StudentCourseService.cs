using System;
using System.Threading.Tasks;
using Exam.Business.Course.Exception;
using Exam.Business.Student.Exception;
using Exam.Domain.Interfaces;

namespace Exam.Business.StudentCourse.Service
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;
        private readonly IStudentCourseMapper studentCourseMapper;

        public StudentCourseService(IReadRepository readRepository, IWriteRepository writeRepository, IStudentCourseMapper studentCourseMapper)
        {
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.studentCourseMapper = studentCourseMapper ?? throw new ArgumentNullException();
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
                throw new ArgumentOutOfRangeException();
            }

            var studentCourse = this.studentCourseMapper.Map(id, studentCourseCreationDto);
            await this.writeRepository.AddNewAsync(studentCourse);
            await this.writeRepository.SaveAsync();
            return this.studentCourseMapper.Map(studentCourse);
        }
    }
}
