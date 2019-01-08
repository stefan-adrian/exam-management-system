using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Student : Entity
    {
        private Student()
        {
            // EF
            Grades = new List<Grade>();
        }

        public string RegistrationNumber { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int YearOfStudy { get; private set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

        public List<Grade> Grades { get; private set; }

        public Student(string registrationNumber, string email, string password, string firstName, string lastName, int yearOfStudy) : base(Guid.NewGuid())
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
            {
                throw new ArgumentException("Registration Number must not be null or have white spaces",
                    "registrationNumber");
            }
            RegistrationNumber = registrationNumber;

            if (string.IsNullOrEmpty(email))
            {
                throw  new ArgumentException("Email adress must not be null","email");
            }
            Email = email;

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must not be null", "password");
            }
            Password = password;

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("FirstName must not be null", "firstName");
            }
            FirstName = firstName;

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("LastName must not be null", "lastName");
            }
            LastName = lastName;

            if (yearOfStudy < 1 || yearOfStudy > 6)
            {
                throw new ArgumentException("Not a valid year of study", "yearOfStudy");
            }
            YearOfStudy = yearOfStudy;

            StudentCourses = new List<StudentCourse>();

            Grades = new List<Grade>();
        }
    }
}
