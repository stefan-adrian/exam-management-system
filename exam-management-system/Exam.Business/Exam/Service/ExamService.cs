using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ExamService(IReadRepository readRepository, IWriteRepository writeRepository, IExamMapper examMapper, ICourseService courseService)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.examMapper = examMapper ?? throw new ArgumentNullException();
            this.courseService = courseService ?? throw new ArgumentNullException();
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
            return examMapper.Map(exam);
        }
    }
}
