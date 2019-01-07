using System;
using System.Collections.Generic;
using System.Text;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Mapper;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mapper
{
    [TestClass]
    public class GradeMapperTest
    {
        private Grade grade;
        private GradeDto gradeDto;
        private GradeCreationDto gradeCreationDto;
        private IGradeMapper gradeMapper;

        [TestInitialize]
        public void Setup()
        {
            this.grade = GradeTestUtils.GetInitialStateGrade();
            this.gradeDto = GradeTestUtils.GetInitialGradeDto(grade.Id);
            this.gradeCreationDto = GradeTestUtils.GetGradeCreationDto();
            this.gradeMapper = new GradeMapper();
        }

        [TestMethod]
        public void Map_ShouldReturnGradeDto_WhenArgumentIsGrade()
        {
            // Act
            var result = this.gradeMapper.Map(grade);
            // Assert
            result.Should().BeEquivalentTo(gradeDto, options => options.Excluding(g => g.Date));
        }

        [TestMethod]
        public void Map_ShouldReturnGrade_WhenArgumentsAreGradeCreationDtoStudentAndExam()
        {
            // Act
            var result = gradeMapper.Map(gradeCreationDto,StudentTestUtils.GetStudent(),ExamTestUtils.GetExam());
            // Assert
            result.Should().BeEquivalentTo(grade,options=>options.Excluding(g=>g.Id));
        }
    }
}
