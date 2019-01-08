using System;

namespace Exam.Business.Grade.Dto
{
    public class GradeEditingDto
    {

        public double Value { get; set; }

        public int Pages { get; set; }

        public DateTime Date { get; set; }

        public bool Agree { get; set; }

        public Guid StudentId { get; set; }

        public Guid ExamId { get; set; }

        public GradeEditingDto(double value, int pages, DateTime date, bool agree, Guid studentId, Guid examId)
        {
            Value = value;
            Pages = pages;
            Date = date;
            Agree = agree;
            StudentId = studentId;
            ExamId = examId;
        }
    }
}
