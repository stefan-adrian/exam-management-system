using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Entities;

namespace Exam.Domain.Interfaces
{
    public interface IWriteRepository
    {
        Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity;

        void Update<TEntity>(TEntity entity)
            where TEntity : Entity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : Entity;

        Task SaveAsync();
    }
}
