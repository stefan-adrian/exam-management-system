using System;
using Exam.Business.Grade.Dto;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class GradeTestUtils
    {
        private static Grade initialStateGrade = null;
        public static DateTime data = DateTime.Now;
        private static Grade gradeWithValue = null;

        public static Grade GetInitialStateGrade()
        {
            if (initialStateGrade == null)
            {
                initialStateGrade = new Grade(StudentTestUtils.GetStudent(), ExamTestUtils.GetExam());
            }
            return initialStateGrade;
        }

        public static GradeDto GetInitialGradeDto(Guid id)
        {
            return new GradeDto(id, 0, 0, data, false, StudentTestUtils.GetStudent().Id, ExamTestUtils.GetExam().Id);
        }

        public static GradeCreationDto GetGradeCreationDto()
        {
            return new GradeCreationDto(StudentTestUtils.GetStudent().Id, ExamTestUtils.GetExam().Id);
        }

        public static GradeEditingDto GetGradeEditingDto()
        {
            return new GradeEditingDto(0,0,data,false,StudentTestUtils.GetStudent().Id,ExamTestUtils.GetExam().Id);
        }

        public static Grade GetGradeWithValue()
        {
            if (gradeWithValue == null)
            {
                gradeWithValue = new Grade(9, 10, DateTime.Now, StudentTestUtils.GetStudent2(), ExamTestUtils.GetExam());
            }
            return gradeWithValue;
        }

        public static GradeDto GetGradeWithValueDto(Guid id, DateTime date)
        {
            return new GradeDto(id, 9, 10, date, false, StudentTestUtils.GetStudent2().Id, ExamTestUtils.GetExam().Id);
        }
    }
}
