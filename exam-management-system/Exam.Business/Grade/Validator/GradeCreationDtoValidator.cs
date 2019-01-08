using Exam.Business.Grade.Dto;
using FluentValidation;

namespace Exam.Business.Grade.Validator
{
    public class GradeCreationDtoValidator : AbstractValidator<GradeCreationDto>
    {
        public GradeCreationDtoValidator()
        {
            RuleFor(gradeCreationDto => gradeCreationDto.StudentId).NotEmpty();
            RuleFor(gradeCreationDto => gradeCreationDto.ExamId).NotEmpty();
        }
    }
}
