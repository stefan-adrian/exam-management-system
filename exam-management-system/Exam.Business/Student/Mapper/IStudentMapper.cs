using System;

namespace Exam.Business.Student
{
    public interface IStudentMapper
    {
        StudentDetailsDto Map(Domain.Entities.Student student);

        StudentDetailsDto Map(Guid id, StudentCreationDto studentCreationDto);

        Domain.Entities.Student Map(StudentCreationDto studentCreationDto);

        Domain.Entities.Student Map(StudentDetailsDto studentDetails, Domain.Entities.Student student);
    }
}
