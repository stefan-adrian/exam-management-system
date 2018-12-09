using System;
using System.Collections.Generic;

namespace Exam.Domain.Entities
{
    public class Exam : Entity
    {
        private Exam()
        {
            // EF
        }

        public DateTime ExamDate { get; private set; }

        public Course Course { get; private set; }

        public List<string> ClassRooms { get; private set; }

        public Exam(DateTime examDate, Course course, List<string> classRooms) : base(Guid.NewGuid())
        {
            ExamDate = examDate;
            Course = course;
            ClassRooms = classRooms;
        }
    }
}
