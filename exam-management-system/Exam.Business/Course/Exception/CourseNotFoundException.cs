using System;

namespace Exam.Business.Course.Exception
{
    public class CourseNotFoundException : System.Exception
    {
        public CourseNotFoundException(Guid id):base("Course with id " + id + " not found!")
        {
        }
    }
}
