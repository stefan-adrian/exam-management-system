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
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Registration Number must not be null or have white spaces",
                    "registrationNumber");
            RegistrationNumber = registrationNumber;

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email adress must not be null", "email");
            Email = email;

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password must not be null", "password");
            Password = password;

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("FirstName must not be null", "firstName");
            FirstName = firstName;

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("LastName must not be null", "lastName");
        }
    }
}
