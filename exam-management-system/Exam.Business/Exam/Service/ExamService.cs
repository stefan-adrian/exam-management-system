using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Exception;
using Exam.Business.Exam.Mapper;
using Exam.Business.Student;
using Exam.Business.StudentCourse.Exception;
using Exam.Business.StudentCourse.Service;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Exam.Service
{
    public class ExamService : IExamService
    {
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;
        private readonly IExamMapper examMapper;
        private readonly ICourseService courseService;
        private readonly IStudentCourseService studentCourseService;
        private readonly IStudentService studentService;

        public ExamService(IReadRepository readRepository, IWriteRepository writeRepository,
            IExamMapper examMapper,
            ICourseService courseService,
            IStudentCourseService studentCourseService,
            IStudentService studentService)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.examMapper = examMapper ?? throw new ArgumentNullException();
            this.courseService = courseService ?? throw new ArgumentNullException();
            this.studentCourseService = studentCourseService ?? throw new ArgumentNullException();
            this.studentService = studentService ?? throw new ArgumentNullException();
        }

        public async Task<Domain.Entities.Exam> GetById(Guid id)
        {
            var exam = await this.readRepository.GetByIdAsync<Domain.Entities.Exam>(id);
            if (exam == null)
            {
                throw new ExamNotFoundException(id);
            }

            return exam;
        }

        public async Task<Domain.Entities.Exam> GetByIdFetchingCourse(Guid id)
        {
            var exam = await this.readRepository.GetAll<Domain.Entities.Exam>().Where(e => e.Id == id)
              .Include(e => e.Course).FirstOrDefaultAsync();
            if (exam == null)
            {
                throw new ExamNotFoundException(id);
            }
            return exam;
        }

        public async Task<ExamDto> GetDtoById(Guid id)
        {
            var exam = await GetByIdFetchingCourse(id);
            return examMapper.Map(exam);
        }

        public async Task<ExamDto> Create(ExamCreatingDto examCreatingDto)
        {
            var course = await courseService.GetCourseById(examCreatingDto.CourseId);
            Domain.Entities.Exam exam = examMapper.Map(examCreatingDto, course);
            await writeRepository.AddNewAsync(exam);
            await writeRepository.SaveAsync();
            return examMapper.Map(exam);
        }

        public async Task<List<ExamDto>> GetAllExamsFromCourseForStudent(Guid courseId, Guid studentId)
        {
            var course = await this.courseService.GetCourseById(courseId);
            var coursesDtos = await this.studentCourseService.GetCourses(studentId);
            var student = await this.studentService.GetStudentById(studentId);
            bool studentAppliedToCourse = false;
            coursesDtos.ForEach(c =>
            {
                if (c.Id == course.Id)
                {
                    studentAppliedToCourse = true;
                }
            });
            if (!studentAppliedToCourse)
            {
                throw new StudentNotAppliedToCourse(student.Id, course.Id);
            }

            return await readRepository.GetAll<Domain.Entities.Exam>().Include(e => e.Course).Where(e => e.Course == course).Select(exam => examMapper.Map(exam)).ToListAsync();
        }
    }
}