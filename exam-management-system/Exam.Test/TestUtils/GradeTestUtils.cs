﻿using System;
using System.Collections.Generic;
using System.Text;
using Exam.Business.Grade.Dto;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class GradeTestUtils
    {
        private static Grade initialStateGrade = null;

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

    }
}
