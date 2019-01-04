using FluentValidation;

namespace Exam.Business.Course.Validator
{
    public class CourseCreatingDtoValidator : AbstractValidator<CourseCreatingDto>
    {
        public CourseCreatingDtoValidator()
        {
            RuleFor(courseCreatingDto => courseCreatingDto.Name).NotEmpty().Length(3, 40);
            RuleFor(courseCreatingDto => courseCreatingDto.Year).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(6); ;

        }
    }
}
