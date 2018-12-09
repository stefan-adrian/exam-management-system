using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Entities;

namespace Exam.Domain.Interfaces
{
    public interface IReadRepository
    {
        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : Entity;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id)
            where TEntity : Entity;
    }
}
