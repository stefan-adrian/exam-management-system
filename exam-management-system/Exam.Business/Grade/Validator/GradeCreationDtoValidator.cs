using Exam.Business.Grade.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

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
