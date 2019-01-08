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
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentException("Location must not be null.", "location");
            }
            Location = location;

            if (capacity < 0)
            {
                throw new ArgumentException("Capacity must not be negative.", "capacity");
            }
            Capacity = capacity;
        }
    }
}
