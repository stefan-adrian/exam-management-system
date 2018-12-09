using System;
using System.Linq;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Persistance
{
    public sealed class ExamContext : DbContext , IReadRepository, IWriteRepository
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
            
        }

        internal DbSet<Student> Students { get; private set; }

        internal DbSet<Professor> Professors { get; private set; }

        internal DbSet<Course> Courses { get; private set; }

        internal DbSet<Domain.Entities.Exam> Exams { get; private set; }

        internal DbSet<Grade> Grades { get; private set; }


        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public async Task AddNewAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
