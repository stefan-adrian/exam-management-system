using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Grade.Dto
{
    public class GradeCreationDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid ExamId { get; set; }

        public GradeCreationDto(Guid studentId, Guid examId)
        {
            StudentId = studentId;
            ExamId = examId;
        }
    }
}
