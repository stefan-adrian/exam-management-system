using Exam.Business.Grade.Dto;
using System;
using System.Threading.Tasks;

namespace Exam.Business.Grade.Service
{
    public interface IGradeService
    {
        Task<GradeDto> Create(GradeCreationDto gradeCreationDto);

        Task<GradeDto> GetStudentExamGrade(Guid studentId, Guid examId);

        Task<GradeDto> Update(Guid existingGradeId, GradeEditingDto gradeEditingDto);

        Task<Domain.Entities.Grade> GetGradeById(Guid id);
    }
}
