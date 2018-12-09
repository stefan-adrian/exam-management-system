using System;
using System.Collections.Generic;
using System.Text;

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
