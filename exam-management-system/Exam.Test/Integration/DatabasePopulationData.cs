﻿using System;
using System.Collections.Generic;
using System.Text;
using Exam.Domain.Entities;
using Exam.Persistance;
using Exam.Test.TestUtils;

namespace Exam.Test.Integration
{
    public static class DatabasePopulationData
    {
        public static async System.Threading.Tasks.Task PopulateTestDatabaseAsync(ExamContext examContext)
        {
            await examContext.AddNewAsync(StudentTestUtils.GetStudent());
            await examContext.AddNewAsync(StudentTestUtils.GetStudent2());
            await examContext.AddNewAsync(CourseTestUtils.GetCourse());
            await examContext.AddNewAsync(CourseTestUtils.GetCourse2());
            await examContext.AddNewAsync(ProfessorTestUtils.GetProfessor());
            await examContext.AddNewAsync(ProfessorTestUtils.GetProfessor2());
            await examContext.SaveAsync();
        }
    }
}