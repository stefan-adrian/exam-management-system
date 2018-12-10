using System;

namespace Exam.Domain.Entities
{
    public class Grade : Entity
    {
        private Grade()
        {

        }

        public int Value { get; private set; }

        public DateTime Date { get; private set; }

        public Student Student { get; private set; }

        public Exam Exam { get; private set; }

        public Grade(int value, DateTime date, Student student, Exam exam) : base(Guid.NewGuid())
        {
            Value = value;
            Date = date;
            Student = student;
            Exam = exam;
        }
    }
}
