﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exam.Business.Exam.Dto;
using Exam.Business.Student;

namespace Exam.Business.Exam.Service
{
    public interface IExamService
    {
        Task<Domain.Entities.Exam> GetById(Guid id);

        Task<ExamDto> GetDtoById(Guid id);

        Task<ExamDto> Create(ExamCreatingDto examCreatingDto);

        Task<List<ExamDto>> GetAllExamsFromCourseForStudent(Guid courseId, Guid studentId);

        Task<List<StudentDetailsDto>> GetCheckedInStudents(Guid examId);
    }
}