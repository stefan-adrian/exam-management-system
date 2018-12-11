using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Student
{
    public interface IStudentMapper
    {
        Domain.Entities.Student Map(StudentCreationDto studentCreationDto);

        StudentDetailsDto Map(Domain.Entities.Student student);
    }
}
