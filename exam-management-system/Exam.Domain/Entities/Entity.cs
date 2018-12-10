using System;

namespace Exam.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {

        }

        public Guid Id { get; private set; }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
