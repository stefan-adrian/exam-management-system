using System;
using Exam.Business.Grade.Dto;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class GradeTestUtils
    {
        private static Grade initialStateGrade = null;
        private static Grade gradeWithValue = null;

        public static Grade GetInitialStateGrade()
        {
            if (initialStateGrade == null)
            {
                initialStateGrade = new Grade(10, StudentTestUtils.GetStudent(), ExamTestUtils.GetExam());
            }
            return initialStateGrade;
        }

        public static GradeDto GetInitialGradeDto(Guid id)
        {
            return new GradeDto(id, 0, 10, DateTime.Now, StudentTestUtils.GetStudent().Id, ExamTestUtils.GetExam().Id);
        }

        public static GradeCreationDto GetGradeCreationDto()
        {
            return new GradeCreationDto(10, StudentTestUtils.GetStudent().Id, ExamTestUtils.GetExam().Id);
        }

        public static Grade GetGradeWithValue()
        {
            if (gradeWithValue == null)
            {
                gradeWithValue = new Grade(9, 10, DateTime.Now, StudentTestUtils.GetStudent(), ExamTestUtils.GetExam());
            }
            return gradeWithValue;
        }

        public static GradeDto GetGradeWithValueDto(Guid id, DateTime date)
        {
            return new GradeDto(id, 9, 10, date, StudentTestUtils.GetStudent().Id, ExamTestUtils.GetExam().Id);
        }
    }
}
