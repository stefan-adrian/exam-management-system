using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Grade.Dto
{
    public class GradeDto
    {
        public double Value { get; set; }

        public int Pages { get; set; }

        public DateTime Date { get; set; }

        public Guid StudentId { get; set; }

        public Guid ExamId { get; set; }

        public GradeDto(double value, int pages, DateTime date, Guid studentId, Guid examId)
        {
            Value = value;
            Pages = pages;
            Date = date;
            StudentId = studentId;
            ExamId = examId;
        }
    }
}
