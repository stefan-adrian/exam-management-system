using Exam.Business.Grade.Dto;
using Exam.Business.Student;

namespace Exam.Business.Grade.Mapper
{
    public class GradeMapper : IGradeMapper
    {
        public Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto,
            Domain.Entities.Student student, Domain.Entities.Exam exam)
        {
            return new Domain.Entities.Grade(student, exam);
        }

        public GradeDto Map(Domain.Entities.Grade grade)
        {
            return new GradeDto(grade.Id, grade.Value, grade.Pages, grade.Date, grade.Student.Id, grade.Exam.Id);
        }

        public GradeFetchingStudentDto Map(Domain.Entities.Grade grade, StudentDetailsDto student)
        {
            return new GradeFetchingStudentDto
            {
                Id = grade.Id,
                Date = grade.Date,
                Pages = grade.Pages,
                Student = student,
                Value = grade.Value
            };
        }
    }
}
