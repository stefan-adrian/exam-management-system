using System;
using Exam.Business.Grade.Dto;

namespace Exam.Business.Student.Dto
{
    public class StudentFetchingGradeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RegistrationNumber { get; set; }

        public int YearOfStudy { get; set; }

        public string Password { get; set; }

        public GradeDto Grade { get; set; }
    }
}
