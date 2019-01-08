using System;

namespace Exam.Domain.Entities
{
    public class ClassroomAllocation : Entity
    {
        private ClassroomAllocation()
        {
            // EF
        }

        public Exam Exam { get; private set; }

        public Classroom Classroom { get; private set; }

        public ClassroomAllocation(Exam exam, Classroom classroom) : base(Guid.NewGuid())
        {
            Exam = exam;
            Classroom = classroom;
        }
    }
}
