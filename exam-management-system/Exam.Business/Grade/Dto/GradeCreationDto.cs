using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exam.Business.Grade.Dto
{
    public class GradeCreationDto
    {
        public int Pages { get; set; }

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
