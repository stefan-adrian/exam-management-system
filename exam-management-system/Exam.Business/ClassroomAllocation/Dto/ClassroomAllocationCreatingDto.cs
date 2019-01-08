using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.ClassroomAllocation
{
    public class ClassroomAllocationCreatingDto
    {
        [Required]
        public Guid ExamId { get; set; }

        [Required]
        public Guid ClassroomId { get; set; }
    }
}
