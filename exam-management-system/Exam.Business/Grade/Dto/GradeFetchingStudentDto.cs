using System;
using Exam.Business.Exam.Dto;
using Exam.Business.Student;

namespace Exam.Business.Grade.Dto
{
    public class GradeFetchingStudentDto
    {
        public Guid Id { get; set; }

        public double Value { get; set; }

        public int Pages { get; set; }

        public DateTime Date { get; set; }

        public StudentDetailsDto Student { get; set; }
    }
}
