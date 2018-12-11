using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Professor
{
    public class ProfessorCreatingDto
    {
        [Required]
        public string RegistrationNumber { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }
    }
}
