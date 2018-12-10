using System;

namespace Exam.Domain.Entities
{
    public class Classroom : Entity
    {
        private Classroom()
        {
            // EF
        }

        public string Location { get; private set; }

        public int Capacity { get; private set; }

        public Classroom(string location, int capacity) : base(Guid.NewGuid())
        {
            Location = location;
            Capacity = capacity;
        }
    }
}
