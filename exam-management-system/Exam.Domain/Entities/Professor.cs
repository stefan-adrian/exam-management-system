using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Professor : Entity
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string RegistrationNumber { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public List<Course> Courses { get; private set; }

        public Professor(string firstName, string lastName, string registrationNumber, string email, string password) : base(Guid.NewGuid())
        {
            FirstName = firstName;
            LastName = lastName;
            RegistrationNumber = registrationNumber;
            Email = email;
            Password = password;
        }
    }
}
