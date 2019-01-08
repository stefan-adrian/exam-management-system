using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Exam.Dto
{
    public class ExamCreatingDto
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public List<Guid> Classrooms { get; set; }
    }
}
