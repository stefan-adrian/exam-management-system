using System;

namespace Exam.Business.Student.Exception
{
    public class StudentNotFoundException : System.Exception
    {
        public StudentNotFoundException(Guid id) : base("Student with id " + id + " not found!")
        {
        }
    }
}
