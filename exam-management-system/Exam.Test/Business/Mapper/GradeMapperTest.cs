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
        private GradeEditingDto gradeEditingDto;
        private IGradeMapper gradeMapper;

        [TestInitialize]
        public void Setup()
        {
            this.grade = GradeTestUtils.GetInitialStateGrade();
            this.gradeDto = GradeTestUtils.GetInitialGradeDto(grade.Id);
            this.gradeCreationDto = GradeTestUtils.GetGradeCreationDto();
            this.gradeEditingDto = GradeTestUtils.GetGradeEditingDto();
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

        [TestMethod]
        public void Map_ShouldReturnGradeDto_WhenArgumentsAreGradeEditingDtoAndId()
        {
            // Act
            var result = this.gradeMapper.Map(this.grade.Id, this.gradeEditingDto);
            // Assert
            result.Should().BeEquivalentTo(this.gradeDto);
        }

        [TestMethod]
        public void Map_ShouldReturnGrade_WhenArgumentsAreGradeDtoAndGrade()
        {
            // Arrange
            var grade = GradeTestUtils.GetInitialStateGrade();
            // Act
            var result = this.gradeMapper.Map(this.gradeDto, grade);
            // Assert
            result.Should().BeEquivalentTo(grade);
        }
    }
}
