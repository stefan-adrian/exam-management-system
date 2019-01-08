﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<GradeDto>> GetAllGradesByExam(Guid examId)
        {
            var exam = await examService.GetById(examId);
            var TOLERANCE = 0.1;
            // the grade must be greater than 0 in order to be marked as set
            var grades = await readRepository.GetAll<Domain.Entities.Grade>()
                .Include(g => g.Student)
                .Include(g => g.Exam)
                .Where(g => g.Exam.Id == exam.Id && Math.Abs(g.Value) > TOLERANCE)
                .Select(g => gradeMapper.Map(g))
                .ToListAsync();
            return grades;
        }
    }
}
