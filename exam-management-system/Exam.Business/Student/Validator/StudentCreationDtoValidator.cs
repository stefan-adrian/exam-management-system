using FluentValidation;

namespace Exam.Business.Student.Validator
{
    public class StudentCreationDtoValidator : AbstractValidator<StudentCreationDto>
    {
        public StudentCreationDtoValidator()
        {
            RuleFor(studentCreationDto => studentCreationDto.FirstName).NotEmpty().Length(5, 20);
        }
    }
}
