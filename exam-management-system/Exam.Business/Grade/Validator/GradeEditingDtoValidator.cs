using Exam.Business.Grade.Dto;
using FluentValidation;

namespace Exam.Business.Grade.Validator
{
    class GradeEditingDtoValidator : AbstractValidator<GradeEditingDto>
    {
        public GradeEditingDtoValidator()
        {
            RuleFor(gradeEditingDto => gradeEditingDto.Value).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
            RuleFor(gradeEditingDto => gradeEditingDto.Agree).NotEmpty();
            RuleFor(gradeEditingDto => gradeEditingDto.Date).NotEmpty();
            RuleFor(gradeEditingDto => gradeEditingDto.Pages).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(30);
            RuleFor(gradeEditingDto => gradeEditingDto.StudentId).NotEmpty();
            RuleFor(gradeEditingDto => gradeEditingDto.ExamId).NotEmpty();
        }
    }
}
