using System;
using Exam.Business.Grade.Dto;
using Exam.Business.Student.Dto;

namespace Exam.Business.Student
{
    public interface IStudentMapper
    {
        Domain.Entities.Student Map(StudentCreationDto studentCreationDto);

        StudentDetailsDto Map(Domain.Entities.Student student);

        StudentDetailsDto Map(Guid id, StudentCreationDto studentCreationDto);

        Domain.Entities.Student Map(StudentDetailsDto studentDetails, Domain.Entities.Student student);

        StudentFetchingGradeDto Map(Domain.Entities.Student student, GradeDto gradeDto);
    }
}
