using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Student
{
    public class StudentCreationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public int YearOfStudy { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
