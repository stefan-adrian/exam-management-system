using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Student
{
    public class StudentMapper : IStudentMapper
    {
        public Domain.Entities.Student Map(StudentCreationDto studentCreationDto)
        {
            return new Domain.Entities.Student(
                studentCreationDto.RegistrationNumber,
                studentCreationDto.Email,
                studentCreationDto.Password,
                studentCreationDto.FirstName,
                studentCreationDto.LastName,
                studentCreationDto.YearOfStudy);
        }

        public StudentDetailsDto Map(Domain.Entities.Student student)
        {
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto();
            studentDetailsDto.Id = student.Id;
            studentDetailsDto.FirstName = student.FirstName;
            studentDetailsDto.LastName = student.LastName;
            studentDetailsDto.Email = student.Email;
            studentDetailsDto.RegistrationNumber = student.RegistrationNumber;
            studentDetailsDto.YearOfStudy = student.YearOfStudy;
            studentDetailsDto.Password = student.Password;
            return studentDetailsDto;
        }
    }
}
