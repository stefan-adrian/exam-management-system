using Exam.Business.Professor;
using Exam.Business.Course;
using Exam.Business.Exam.Mapper;
using Exam.Business.Exam.Service;
using Exam.Business.Student;
using Exam.Business.StudentCourse;
using Exam.Business.StudentCourse.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentMapper, StudentMapper>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IProfessorMapper, ProfessorMapper>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseMapper, CourseMapper>();
            services.AddScoped<IStudentCourseService, StudentCourseService>();
            services.AddScoped<IStudentCourseMapper, StudentCourseMapper>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamMapper, ExamMapper>();

            return services;
        }
    }
}
