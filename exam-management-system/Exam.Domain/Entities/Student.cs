using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Student : Entity
    {
        private Student()
        {
            // EF
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string RegistrationNumber { get; private set; }

        public int YearOfStudy { get; private set; }

        public string Password { get; private set; }

        public List<Course> Courses { get; private set; }

        public List<Grade> Grades { get; private set; }

        public Student(string firstName, string lastName, string email, string registrationNumber, int yearOfStudy, string password) : base(Guid.NewGuid())
        {

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RegistrationNumber = registrationNumber;
            YearOfStudy = yearOfStudy;
            Password = password;
        }
    }
}
