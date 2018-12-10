using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Professor : Entity
    {
        public string RegistrationNumber { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<Course> Courses { get; private set; }

        public Professor(string registrationNumber, string email, string password, string firstName, string lastName) : base(Guid.NewGuid())
        {
            RegistrationNumber = registrationNumber;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
