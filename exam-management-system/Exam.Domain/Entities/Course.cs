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

        public ICollection<StudentCourse> StudentCourses { get; set; }

        public List<Exam> Exams { get; private set; }

        public Course(string name, int year, Professor professor) : base(Guid.NewGuid())
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name of course must not be null", "name");
            }
            Name = name;

            if (year < 1 || year > 6)
            {
                throw new ArgumentException("Not a valid year", "year");
            }
            Year = year;

            if (professor == null)
            {
                throw new ArgumentException("A course must be assigned to a professor", "professor");
            }
            Professor = professor;

            StudentCourses=new List<StudentCourse>();
        }

        public Course(string name, int year) : base(Guid.NewGuid())
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name of course must not be null", "name");
            }
            Name = name;

            if (year < 1 || year > 6)
            {
                throw new ArgumentException("Not a valid year", "year");
            }
            Year = year;
        }

        public Course(string name, int year, List<Exam> exams) : base(Guid.NewGuid())
        {
            Name = name;
            Year = year;
            Exams = exams;
        }
    }
}
