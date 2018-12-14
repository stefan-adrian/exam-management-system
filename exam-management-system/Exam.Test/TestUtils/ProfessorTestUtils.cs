using System;
using Exam.Business.Professor;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class ProfessorTestUtils
    {
        public static Professor GetProfessor()
        {
            return new Professor("registrationNumber","email@email.ro","password","FirstName","LastName");
        }

        public static ProfessorDetailsDto GetProfessorDetailsDto(Guid id)
        {
            return new ProfessorDetailsDto
            {
                Id = id,
                Email = "email@email.ro",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password",
                RegistrationNumber = "registrationNumber"
            };
        }

        public static ProfessorCreatingDto GetProfessorCreatingDto()
        {
            return new ProfessorCreatingDto
            {
                Email = "email@email.ro",
                FirstName = "FirstName",
                LastName = "LastName",
                Password = "password",
                RegistrationNumber = "registrationNumber"
            };
        }
    }
}
