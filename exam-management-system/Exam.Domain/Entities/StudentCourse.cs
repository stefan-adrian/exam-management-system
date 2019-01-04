using System;

namespace Exam.Domain.Entities
{
    public class StudentCourse : Entity
    {
        private StudentCourse()
        {
            // EF
        }

        public Guid StudentId { get; private set; }

        public Student Student { get; private set; }

        public Guid CourseId { get; private set; }

        public Course Course { get; private set; }

        public StudentCourse(Guid studentId, Guid courseId) : base(Guid.NewGuid())
        {
            this.StudentId = studentId;
            this.CourseId = courseId;
        }
    }
}
