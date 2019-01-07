using System;

namespace Exam.Business.StudentCourse.Exception
{
    public class StudentNotAppliedToCourse : System.Exception
    {
        public StudentNotAppliedToCourse(Guid studentId, Guid courseId) : base("Student with id " + studentId + " did not apply to course with id " + courseId + "!")
        {
        }
    }
}
