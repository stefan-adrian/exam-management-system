using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Exam : Entity
    {
        private Exam()
        {
            // EF
        }

        public DateTime Date { get; private set; }

        public Course Course { get; private set; }

        public List<ClassroomAllocation> ClassroomAllocation { get; private set; }

        public Exam(DateTime date, Course course) : base(Guid.NewGuid())
        {
            Date = date;
            Course = course;
        }
    }
}
