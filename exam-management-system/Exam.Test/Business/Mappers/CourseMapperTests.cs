using Exam.Business.Course;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mappers
{
    [TestClass]
    public class CourseMapperTests
    {
        private Course _course;
        private CourseDto _courseDetailsDto;
        private CourseCreatingDto _courseCreatingDto;
        private ICourseMapper _courseMapper;

        [TestInitialize]
        public void Setup()
        {
            this._course = CourseTestUtils.GetCourse();
            this._courseDetailsDto = CourseTestUtils.GetCourseDetailsDto(this._course.Id);
            this._courseCreatingDto = CourseTestUtils.GetCourseCreatingDto();
            this._courseMapper = new CourseMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._course = null;
            this._courseDetailsDto = null;
            this._courseCreatingDto = null;
            this._courseMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnCourseDetailsDto_WhenArgumentIsCourse()
        {
            // Act
            var result = this._courseMapper.Map(this._course);
            // Assert
            result.Should().BeEquivalentTo(this._courseDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnCourse_WhenArgumentIsCourseCreatingDto()
        {
            // Act
            var result = this._courseMapper.Map(this._courseCreatingDto);
            // Assert
            result.Should().Match<Course>((obj) =>
                obj.Name == this._course.Name &&
                obj.Year == this._course.Year);
        }

        [TestMethod]
        public void Map_ShouldReturnCourse_WhenArgumentsAreCourseDetailsDtoAndCourse()
        {
            // Arrange
            this._courseDetailsDto.Name = "newCourse";
            // Act
            var result = this._courseMapper.Map(this._courseDetailsDto, this._course);
            // Assert
            result.Should().Match<Course>((obj) =>
                obj.Name == this._courseDetailsDto.Name);
        }

        [TestMethod]
        public void Map_ShouldReturnCourseDetailsDto_WhenArgumentsAreCourseCreatingDtoAndId()
        {
            // Act
            var result = this._courseMapper.Map(this._course.Id, this._courseCreatingDto);
            // Assert
            result.Should().BeEquivalentTo(this._courseDetailsDto);
        }
    }
}
