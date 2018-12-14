using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Exam.Business.Course.Validator
{
    public class CourseCretingDtoValidator : AbstractValidator<CourseCreatingDto>
    {
        public CourseCretingDtoValidator()
        {
            RuleFor(courseCreatingDto => courseCreatingDto.Name).NotEmpty().Length(3, 40);
            RuleFor(courseCreatingDto => courseCreatingDto.Year).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(6); ;

        }
    }
}
