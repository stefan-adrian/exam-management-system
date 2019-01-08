using System;
using Exam.Business.Grade.Dto;
using Exam.Business.Student;
using Exam.Business.Student.Dto;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class StudentTestUtils
    {
        private static Student student1 = null;
        private static Student student2 = null;

        public static Student GetStudent()
        {
            if (student1 == null)
            {
                student1 = new Student("registrationNumber", "email@email.ro", "password", "FirstName", "LastName", 1);
            }

            return student1;
        }

        public static Student GetStudent2()
        {
            if (student2 == null)
            {
                student2 = new Student("registrationNumber", "email@email.ro", "password", "FirstName", "LastName", 1);
            }

            return student2;
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

        public static StudentFetchingGradeDto GetStudentFetchingGradeDto(Guid id, GradeDto grade)
        {
            return new StudentFetchingGradeDto
            {
                Id = id,
                Email = "email@email.ro",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password",
                RegistrationNumber = "registrationNumber",
                YearOfStudy = 1,
                Grade = grade
            };
        }
    }
}
