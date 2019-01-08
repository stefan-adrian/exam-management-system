using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Exam.Business.Exam.Service;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Exception;
using Exam.Business.Grade.Mapper;
using Exam.Business.Student;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Grade.Service
{
    public class GradeService : IGradeService
    {
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;
        private readonly IGradeMapper gradeMapper;
        private readonly IStudentService studentService;
        private readonly IExamService examService;

        public GradeService(IReadRepository readRepository, IWriteRepository writeRepository,
            IGradeMapper gradeMapper, IStudentService studentService, IExamService examService)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.gradeMapper = gradeMapper;
            this.studentService = studentService;
            this.examService = examService;
        }

        public async Task<GradeDto> Create(GradeCreationDto gradeCreationDto)
        {
            var exam = await examService.GetById(gradeCreationDto.ExamId);
            var student = await studentService.GetStudentById(gradeCreationDto.StudentId);
            Domain.Entities.Grade grade = gradeMapper.Map(gradeCreationDto, student, exam);
            student.Grades.Add(grade);
            await writeRepository.AddNewAsync(grade);
            await writeRepository.SaveAsync();
            return gradeMapper.Map(grade);
        }

        public async Task<GradeDto> Update(Guid existingGradeId, GradeEditingDto gradeEditingDto)
        {
            GradeDto gradeDto = this.gradeMapper.Map(existingGradeId, gradeEditingDto);
            var grade = GetGradeById(existingGradeId).Result;
            this.writeRepository.Update(this.gradeMapper.Map(gradeDto,grade));
            await this.writeRepository.SaveAsync();
            return gradeDto;
        }

        public async Task<GradeDto> GetStudentExamGrade(Guid studentId, Guid examId)
        {
            var grade = await readRepository.GetAll<Domain.Entities.Grade>()
                .Include(g => g.Student)
                .Include(g => g.Exam)
                .Where(g => g.Student.Id == studentId)
                .Where(g => g.Exam.Id == examId)
                .FirstOrDefaultAsync();
            if (grade == null)
            {
                throw new GradeNotFoundException(studentId, examId);
            }

            return gradeMapper.Map(grade);
        }

        public async Task<Domain.Entities.Grade> GetGradeById(Guid id)
        {
            var grade = await this.readRepository.GetByIdAsync<Domain.Entities.Grade>(id);
            if (grade == null)
            {
                throw new CourseNotFoundException(id);
            }

            return grade;
        }
    }
}
