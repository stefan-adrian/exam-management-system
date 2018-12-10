using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Student : Entity
    {
        private Student()
        {
            // EF
        }

        public string RegistrationNumber { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int YearOfStudy { get; private set; }

        public List<Course> Courses { get; private set; }

        public List<Grade> Grades { get; private set; }

        public Student(string registrationNumber, string email, string password, string firstName, string lastName, int yearOfStudy) : base(Guid.NewGuid())
        {
            RegistrationNumber = registrationNumber;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            YearOfStudy = yearOfStudy;
        }
    }
}
