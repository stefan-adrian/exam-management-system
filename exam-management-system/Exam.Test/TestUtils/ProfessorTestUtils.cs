using System;
using Exam.Business.Professor;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class ProfessorTestUtils
    {
        private static Professor professor1 = null;
        private static Professor professor2 = null;

        public static Professor GetProfessor()
        {
            if (professor1 == null)
            {
                professor1 = new Professor("registrationNumber", "email@email.ro", "password", "FirstName", "LastName");
            }

            return professor1;
        }

        public static Professor GetProfessor2()
        {
            if (professor2 == null)
            {
                professor2 = new Professor("registrationNumber", "email@email.ro", "password", "FirstName", "LastName");
            }

            return professor2;
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
