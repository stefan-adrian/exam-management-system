using Exam.Business.Exam.Dto;

namespace Exam.Business.Exam.Mapper
{
    public interface IExamMapper
    {
        Domain.Entities.Exam Map(ExamCreatingDto examCreatingDto, Domain.Entities.Course course);

        ExamDto Map(Domain.Entities.Exam exam);
    }
}
