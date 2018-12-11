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

            return services;
        }
    }
}
