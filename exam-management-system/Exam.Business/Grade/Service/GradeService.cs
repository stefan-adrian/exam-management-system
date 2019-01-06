using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Exam.Service;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Mapper;
using Exam.Business.Student;
using Exam.Domain.Interfaces;

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
    }
}
