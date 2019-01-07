using FluentValidation;

namespace Exam.Business.Classroom.Validator
{
    public class ClassroomCreatingDtoValidator : AbstractValidator<ClassroomCreatingDto>
    {
        public ClassroomCreatingDtoValidator()
        {
            RuleFor(classroomCreatingDto => classroomCreatingDto.Location).NotEmpty().Length(2, 20);
            RuleFor(classroomCreatingDto => classroomCreatingDto.Capacity).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
