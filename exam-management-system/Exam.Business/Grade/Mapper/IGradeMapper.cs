using Exam.Business.Grade.Dto;
using Exam.Business.Student;

namespace Exam.Business.Grade.Mapper
{
    public interface IGradeMapper
    {
        Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto, Domain.Entities.Student student,
            Domain.Entities.Exam exam);

        GradeDto Map(Domain.Entities.Grade grade);

        GradeFetchingStudentDto Map(Domain.Entities.Grade grade, StudentDetailsDto student);
    }
}
