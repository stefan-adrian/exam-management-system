using System;

namespace Exam.Business.ClassroomAllocation.Exception
{
    public class ClassroomAllocationNotFound : System.Exception
    {
        public ClassroomAllocationNotFound(Guid id) : base("ClassroomAllocation with id " + id + " not found!")
        {
            
        }
    }
}
