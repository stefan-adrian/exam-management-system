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
            => Set<TEntity>().AsNoTracking();

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : Entity
            => await Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);

        public async Task AddNewAsync<TEntity>(TEntity entity) where TEntity : Entity
            => await Set<TEntity>().AddAsync(entity);

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
            => Set<TEntity>().Update(entity);

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
            => Set<TEntity>().Remove(entity);

        public async Task SaveAsync() => await SaveChangesAsync();
    }
}
