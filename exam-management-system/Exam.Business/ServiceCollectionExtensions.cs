using Exam.Business.Professor;
using Exam.Business.Course;
using Exam.Business.Student;
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

            return services;
        }
    }
}
