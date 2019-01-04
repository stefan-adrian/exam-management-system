using System;

namespace Exam.Business.StudentCourse.Exception
{
    public class StudentCannotApplyException : System.Exception
    {
        public StudentCannotApplyException(Guid studentId, Guid courseId) : base("Student with id " + studentId + " cannot apply to course with id " + courseId + "!")
        {
        }
    }
}
