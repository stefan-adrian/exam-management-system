using System;

namespace Exam.Business.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public CourseDto(Guid id, string name, int year)
        {
            Id = id;
            Name = name;
            Year = year;
        }
    }
}
