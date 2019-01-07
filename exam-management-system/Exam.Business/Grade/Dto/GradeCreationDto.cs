using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exam.Business.Grade.Dto
{
    public class GradeCreationDto
    {
        [Required]
        public int Pages { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid ExamId { get; set; }

        public GradeCreationDto(int pages, Guid studentId, Guid examId)
        {
            Pages = pages;
            StudentId = studentId;
            ExamId = examId;
        }
    }
}
