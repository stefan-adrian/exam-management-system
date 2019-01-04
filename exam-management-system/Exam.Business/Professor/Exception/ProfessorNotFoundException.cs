using System;

namespace Exam.Business.Professor.Exception
{
    public class ProfessorNotFoundException : System.Exception
    {
        public ProfessorNotFoundException(Guid id):base("Professor with id " + id + " not found!")
        {
            
        }
    }
}
