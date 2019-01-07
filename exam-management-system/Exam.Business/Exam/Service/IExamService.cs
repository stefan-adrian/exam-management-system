using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exam.Business.Exam.Dto;

namespace Exam.Business.Exam.Service
{
    public interface IExamService
    {
        Task<ExamDto> GetById(Guid id);

        Task<ExamDto> Create(ExamCreatingDto examCreatingDto);

        Task<List<ExamDto>> GetAllExamsFromCourseForStudent(Guid courseId, Guid studentId);
    }
}
