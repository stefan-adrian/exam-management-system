using System;

namespace Exam.Business.Classroom.Exception
{
    public class ClassroomNotFoundException : System.Exception
    {
        public ClassroomNotFoundException(Guid id) : base("Classroom with id " + id + " not found!")
        {
            
        }
    }
}
