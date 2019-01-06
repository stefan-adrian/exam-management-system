using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Exam.Business.Exam.Dto
{
    public class ExamDto
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public DateTime Date { get; set; }

        public ExamDto(Guid id, Guid courseId, DateTime date)
        {
            Id = id;
            CourseId = courseId;
            Date = date;
        }
    }
}
