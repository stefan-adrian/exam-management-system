using Exam.Business.Grade.Dto;

namespace Exam.Business.Grade.Mapper
{
    public interface IGradeMapper
    {
        Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto, Domain.Entities.Student student,
            Domain.Entities.Exam exam);

        GradeDto Map(Domain.Entities.Grade grade);
    }
}
