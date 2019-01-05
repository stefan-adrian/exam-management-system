using System;
using System.Collections.Generic;
using System.Text;
using Exam.Business.Course;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Mapper;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mapper
{
    [TestClass]
    public class ExamMapperTests
    {
        private Domain.Entities.Exam _exam;
        private ExamDto _examDto;
        private ExamCreatingDto _examCreatingDto;
        private IExamMapper _examMapper;

        [TestInitialize]
        public void Setup()
        {
            this._exam = ExamTestUtils.GetExam();
            this._examDto = ExamTestUtils.GetExamDto(this._exam.Id);
            this._examCreatingDto = ExamTestUtils.GetExamCreatingDto();
            this._examMapper = new ExamMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._exam = null;
            this._examDto = null;
            this._examCreatingDto = null;
            this._examMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnExamDto_WhenArgumentIsExam()
        {
            // Act
            var result = this._examMapper.Map(this._exam);
            // Assert
            result.Should().BeEquivalentTo(this._examDto);
        }

        [TestMethod]
        public void Map_ShouldReturnExam_WhenArgumentsAreExamCreatingDtoAndCourse()
        {
            // Act
            var result = this._examMapper.Map(this._examCreatingDto, CourseTestUtils.GetCourse());
            // Assert
            result.Should().Match<Domain.Entities.Exam>((obj) =>
                obj.Date == this._exam.Date &&
                obj.Course == this._exam.Course);
        }
    }
}
