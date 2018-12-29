using System;

namespace Exam.Business.StudentCourse.Exception
{
    public class StudentAlreadyAppliedException : System.Exception
    {
        public StudentAlreadyAppliedException(Guid studentId, Guid courseId) : base("Student with id " + studentId + " already applied to course with id " + courseId + "!")
        {
        }
    }
}
