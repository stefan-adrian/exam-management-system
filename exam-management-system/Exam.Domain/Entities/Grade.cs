using System;

namespace Exam.Domain.Entities
{
    public class Grade : Entity
    {
        private Grade()
        {

        }

        public double Value { get; private set; }

        public int Pages { get; private set; }

        public DateTime Date { get; private set; }

        public Student Student { get; private set; }

        public Exam Exam { get; private set; }

        public Grade(int pages, Student student, Exam exam) : base(Guid.NewGuid())
        {
            Pages = pages;
            Student = student;
            Exam = exam;
        }
    }
}
