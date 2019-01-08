using System;
using System.Globalization;
using Exam.Business.Exam.Dto;

namespace Exam.Test.TestUtils
{
    public class ExamTestUtils
    {
        private static Domain.Entities.Exam exam = null;
        private static CultureInfo MyCultureInfo = new CultureInfo("de-DE");
        private static string MyString = "12 Juni 2008";
        private static DateTime MyDateTime = DateTime.Parse(MyString, MyCultureInfo);

        public static Domain.Entities.Exam GetExam()
        {
            if (exam == null)
            {
                exam = new Domain.Entities.Exam(MyDateTime, CourseTestUtils.GetCourse());
            }

            return exam;
        }

        public static ExamDto GetExamDto(Guid id)
        {
            return new ExamDto(id, CourseTestUtils.GetCourse().Id, MyDateTime);
        }

        public static ExamCreatingDto GetExamCreatingDto()
        {
            return new ExamCreatingDto
            {
                CourseId = CourseTestUtils.GetCourse().Id,
                Date = MyDateTime
            };
        }
    }
}
