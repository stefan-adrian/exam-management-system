using System;
using Exam.Business.Student;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class StudentTestUtils
    {
        public static Student GetStudent()
        {
            return new Student("registrationNumber","email@email.ro","password","FirstName","LastName", 1);
        }

        public static StudentDetailsDto GetStudentDetailsDto(Guid id)
        {
            return new StudentDetailsDto()
            {
                Id = id,
                Email = "email@email.ro",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password",
                RegistrationNumber = "registrationNumber",
                YearOfStudy = 1
            };
        }

        public static StudentCreationDto GetStudentCreationDto()
        {
            return new StudentCreationDto()
            {
                Email = "email@email.ro",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password",
                RegistrationNumber = "registrationNumber",
                YearOfStudy = 1
            };
        }
    }
}
