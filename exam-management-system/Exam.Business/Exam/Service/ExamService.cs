using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.ClassroomAllocation;
using Exam.Business.Course;
using Exam.Business.Email;
using Exam.Business.Email.EmailFormat;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Exception;
using Exam.Business.Exam.Mapper;
using Exam.Business.Grade.Mapper;
using Exam.Business.Student;
using Exam.Business.Student.Dto;
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
        private readonly IClassroomAllocationService classroomAllocationService;
        private readonly IClassroomAllocationMapper classroomAllocationMapper;
        private readonly IGradeMapper gradeMapper;
        private readonly IStudentMapper studentMapper;
        private readonly IEmailService emailService;

        public ExamService(IReadRepository readRepository, IWriteRepository writeRepository,
            IExamMapper examMapper,
            ICourseService courseService,
            IStudentCourseService studentCourseService,
            IStudentService studentService,
            IClassroomAllocationService classroomAllocationService,
            IClassroomAllocationMapper classroomAllocationMapper,
            IGradeMapper gradeMapper,
            IStudentMapper studentMapper,
            IEmailService emailService)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.examMapper = examMapper ?? throw new ArgumentNullException();
            this.courseService = courseService ?? throw new ArgumentNullException();
            this.classroomAllocationMapper = classroomAllocationMapper ?? throw new ArgumentNullException();
            this.classroomAllocationService = classroomAllocationService ?? throw new ArgumentNullException();
            this.studentCourseService = studentCourseService ?? throw new ArgumentNullException();
            this.studentService = studentService ?? throw new ArgumentNullException();
            this.gradeMapper = gradeMapper ?? throw new ArgumentNullException();
            this.studentMapper = studentMapper ?? throw new ArgumentNullException();
            this.emailService = emailService ?? throw new ArgumentNullException();
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

            var classroomAllocations = classroomAllocationMapper.Map(examCreatingDto, exam.Id);

            foreach (var ca in classroomAllocations)
            {
                await classroomAllocationService.Create(ca);
            }
            await this.SendExamCreatedEmail(exam.Id);

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

        public async Task<List<StudentFetchingGradeDto>> GetCheckedInStudents(Guid examId)
        {
            var exam = await this.readRepository.GetByIdAsync<Domain.Entities.Exam>(examId);

            if (exam == null)
            {
                throw new ExamNotFoundException(examId);
            }

            var grades = await this.readRepository.GetAll<Domain.Entities.Grade>().Include(g => g.Student)
                .Include(g => g.Exam).Where(g => g.Exam.Id == examId).ToListAsync();

            return grades.Select(g => studentMapper.Map(g.Student, gradeMapper.Map(g))).ToList();
        }

        public async Task<List<ExamDto>> GetAllExamsForACourse(Guid courseId)
        {
            var exams = new List<ExamDto>();
            var course = await this.readRepository.GetAll<Domain.Entities.Course>().Where(c => c.Id == courseId)
                .Include(c => c.Exams).FirstOrDefaultAsync();

            foreach (var exam in course.Exams)
            {
                exams.Add(examMapper.Map(exam));
            }

            return exams;
        }

        private async Task SendExamCreatedEmail(Guid examId)
        {
            var examFetched = await readRepository.GetAll<Domain.Entities.Exam>().Where(e => e.Id == examId)
                .Include(e => e.Course)
                .Include(e => e.ClassroomAllocation).ThenInclude(ca => ca.Classroom).FirstOrDefaultAsync();
            var students = await readRepository.GetAll<Domain.Entities.Student>().Include(s => s.StudentCourses)
                .Where(s => s.StudentCourses.Any(sc => sc.CourseId == examFetched.Course.Id))
                .ToListAsync();
            foreach (var student in students)
            {
                try
                {
                    emailService.SendEmail(new ExamCreatedEmail(student.Email, examFetched));
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private async Task SendBaremAddedEmail(Guid examId)
        {
            var exam = await readRepository.GetAll<Domain.Entities.Exam>().Where(e => e.Id == examId)
                .Include(e => e.Course).FirstOrDefaultAsync();
            var students = await readRepository.GetAll<Domain.Entities.Student>().Include(s => s.StudentCourses)
                .Where(s => s.StudentCourses.Any(sc => sc.CourseId == exam.Course.Id))
                .ToListAsync();
            foreach (var student in students)
            {
                try
                {
                    emailService.SendEmail(new BaremAddedEmail(student.Email, exam));
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}