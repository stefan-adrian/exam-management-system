using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Exam.Business.Professor.Validator
{
    public class ProfessorCreatingDtoValidator : AbstractValidator<ProfessorCreatingDto>
    {
        public ProfessorCreatingDtoValidator()
        {
            RuleFor(professorCreatingDto => professorCreatingDto.FirstName).NotEmpty().Length(5, 10);
        }
    }
}
