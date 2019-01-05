using FluentValidation;

namespace Exam.Business.Professor.Validator
{
    public class ProfessorCreatingDtoValidator : AbstractValidator<ProfessorCreatingDto>
    {
        public ProfessorCreatingDtoValidator()
        {
            RuleFor(professorCreatingDto => professorCreatingDto.RegistrationNumber).NotEmpty().Length(4, 20);
            RuleFor(professorCreatingDto => professorCreatingDto.Email).NotEmpty().EmailAddress();
            RuleFor(professorCreatingDto => professorCreatingDto.Password).NotEmpty().Length(6, 50);
            RuleFor(professorCreatingDto => professorCreatingDto.FirstName).NotEmpty().Length(2, 30);
            RuleFor(professorCreatingDto => professorCreatingDto.FirstName).NotEmpty().Length(2, 30);
        }
    }
}