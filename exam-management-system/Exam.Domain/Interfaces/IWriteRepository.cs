using System.Threading.Tasks;
using Exam.Domain.Entities;

namespace Exam.Domain.Interfaces
{
    public interface IWriteRepository
    {
        Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task DeleteAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task SaveAsync();
    }
}
