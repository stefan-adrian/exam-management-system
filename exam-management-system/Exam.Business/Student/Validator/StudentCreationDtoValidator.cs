using FluentValidation;

namespace Exam.Business.Student.Validator
{
    public class StudentCreationDtoValidator : AbstractValidator<StudentCreationDto>
    {
        public StudentCreationDtoValidator()
        {
            RuleFor(studentCreationDto => studentCreationDto.FirstName).NotEmpty().Length(2, 30);
            RuleFor(studentCreationDto => studentCreationDto.LastName).NotEmpty().Length(2, 30);
            RuleFor(studentCreationDto => studentCreationDto.Email).NotEmpty().EmailAddress();
            RuleFor(studentCreationDto => studentCreationDto.RegistrationNumber).NotEmpty().Length(4, 20);
            RuleFor(studentCreationDto => studentCreationDto.YearOfStudy).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(6);
            RuleFor(studentCreationDto => studentCreationDto.Password).NotEmpty().Length(6, 50);
        }
    }
}