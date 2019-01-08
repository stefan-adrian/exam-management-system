using System;

namespace Exam.Business.Classroom
{
    public class ClassroomDetailsDto
    {
        public Guid Id { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }
    }
}
