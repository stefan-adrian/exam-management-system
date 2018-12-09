using System;
using System.Collections.Generic;
using System.Text;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Persistance
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ExamContext>(opt =>
                opt.UseSqlServer(connectionString));

            services.AddScoped<IReadRepository>(provider => provider.GetService<ExamContext>());
            services.AddScoped<IWriteRepository>(provider => provider.GetService<ExamContext>());

            return services;
        }
    }
}
