using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exam.Business.Exam.Dto
{
    public class ExamCreatingDto
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
