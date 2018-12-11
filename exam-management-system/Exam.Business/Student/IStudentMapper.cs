﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Student
{
    public interface IStudentMapper
    {
        Domain.Entities.Student Map(StudentCreationDto studentCreationDto);

        StudentDetailsDto Map(Domain.Entities.Student student);

        StudentDetailsDto Map(Guid id, StudentCreationDto studentCreationDto);

        Domain.Entities.Student map(StudentDetailsDto studentDetails, Domain.Entities.Student student);
    }
}
