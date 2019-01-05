using FluentValidation;

namespace Exam.Business.StudentCourse
{
    public class StudentCourseCreationDtoValidator : AbstractValidator<StudentCourseCreationDto>
    {
        public StudentCourseCreationDtoValidator()
        {
            RuleFor(studentCourseCreationDto => studentCourseCreationDto.CourseId).NotEmpty().NotNull();
        }
    }
}