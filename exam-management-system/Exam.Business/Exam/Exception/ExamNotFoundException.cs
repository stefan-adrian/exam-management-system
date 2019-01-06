using System;

namespace Exam.Business.Exam.Exception
{
    public class ExamNotFoundException : System.Exception
    {
        public ExamNotFoundException(Guid id) : base("Exam with id " + id + " not found!")
        {
        }
    }
}
