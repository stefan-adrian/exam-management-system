using System;

namespace Exam.Business.ClassroomAllocation
{
    public class ClassroomAllocationDetailsDto
    {
        public Guid Id { get; set; }

        public Guid ExamId { get; set; }

        public Guid ClassroomId { get; set; }
    }
}
