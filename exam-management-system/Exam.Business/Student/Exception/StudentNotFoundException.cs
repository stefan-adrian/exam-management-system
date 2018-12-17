using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Transactions;

namespace Exam.Business.Student.Exception
{
    public class StudentNotFoundException : System.Exception
    {
        public StudentNotFoundException(Guid id) : base("Student with id " + id + " not found!")
        {
        }
    }
}
