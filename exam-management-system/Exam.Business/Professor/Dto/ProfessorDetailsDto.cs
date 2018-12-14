using System;

namespace Exam.Business.Professor
{
    public class ProfessorDetailsDto
    {
        public Guid Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
