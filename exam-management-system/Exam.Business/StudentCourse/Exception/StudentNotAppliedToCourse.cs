using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.StudentCourse.Exception
{
    class StudentNotAppliedToCourse : System.Exception
    {
        public StudentNotAppliedToCourse(Guid studentId, Guid courseId) : base("Student with id " + studentId + " did not apply to course with id " + courseId + "!")
        {
        }
    }
}
