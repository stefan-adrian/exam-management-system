using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Grade : Entity
    {
        private Grade()
        {

        }

        public int Value { get; private set; }

        public Student Student { get; private set; }

        public Course Course { get; private set; }

        public DateTime Date { get; private set; }

        public Grade(int value, Student student, Course course, DateTime date) : base(Guid.NewGuid())
        {
            Value = value;
            Student = student;
            Course = course;
            Date = date;
        }
    }
}
