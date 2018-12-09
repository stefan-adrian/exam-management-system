using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RegistrationNumber { get; set; }

        public int YearOfStudy { get; set; }

        public string Password { get; set; }

        //        public List<Course> Courses { get; set; }

        //        public List<Grade> Grades { get; set; }
    }
}