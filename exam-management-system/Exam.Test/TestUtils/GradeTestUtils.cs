using System;
using Exam.Business.Grade.Dto;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class GradeTestUtils
    {
        private static Grade initialStateGrade = null;
        public static DateTime data = DateTime.Now;

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

    }
}
