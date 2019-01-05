using System;
using System.Collections.Generic;
using System.Text;
using Exam.Business.Exam.Dto;

namespace Exam.Business.Exam.Mapper
{
    public class ExamMapper : IExamMapper
    {
        public Domain.Entities.Exam Map(ExamCreatingDto examCreatingDto, Domain.Entities.Course course)
        {
            return new Domain.Entities.Exam(examCreatingDto.Date, course);
        }

        public ExamDto Map(Domain.Entities.Exam exam)
        {
            return new ExamDto(exam.Id, exam.Course.Id, exam.Date);
        }
    }
}
