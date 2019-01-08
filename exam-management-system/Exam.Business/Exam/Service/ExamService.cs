using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Business.ClassroomAllocation;
using Exam.Business.Course;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Exception;
using Exam.Business.Exam.Mapper;
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
        private readonly IClassroomAllocationService classroomAllocationService;
        private readonly IClassroomAllocationMapper classroomAllocationMapper;

        public ExamService(IReadRepository readRepository, IWriteRepository writeRepository, IExamMapper examMapper, ICourseService courseService, IClassroomAllocationService classroomAllocationService, IClassroomAllocationMapper classroomAllocationMapper)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.examMapper = examMapper ?? throw new ArgumentNullException();
            this.courseService = courseService ?? throw new ArgumentNullException();
            this.classroomAllocationMapper = classroomAllocationMapper ?? throw new ArgumentNullException();
            this.classroomAllocationService = classroomAllocationService ?? throw new ArgumentNullException();
        }

        public async Task<ExamDto> GetById(Guid id)
        {
            var exam = await this.readRepository.GetAll<Domain.Entities.Exam>().Where(e => e.Id == id)
                .Include(e => e.Course).FirstOrDefaultAsync();
            if (exam == null)
            {
                throw new ExamNotFoundException(id);
            }
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

            return examMapper.Map(exam);
        }
    }
}
