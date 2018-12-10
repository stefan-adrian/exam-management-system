using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Course : Entity
    {
        private Course()
        {
            // EF
        }

        public string Name { get; private set; }

        public int Year { get; private set; }

        public Professor Professor { get; private set; }

        public List<Student> Students { get; private set; }

        public List<Exam> Exams { get; private set; }

        public Course(string name, int year, Professor professor) : base(Guid.NewGuid())
        {
            Name = name;
            Year = year;
            Professor = professor;
        }
    }
}
