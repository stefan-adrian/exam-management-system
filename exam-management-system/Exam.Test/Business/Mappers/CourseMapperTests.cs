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
        private Course course;
        private CourseDto courseDetailsDto;
        private CourseCreatingDto courseCreatingDto;
        private ICourseMapper courseMapper;

        [TestInitialize]
        public void Setup()
        {
            this.course = CourseTestUtils.GetCourse();
            this.courseDetailsDto = CourseTestUtils.GetCourseDetailsDto(this.course.Id);
            this.courseCreatingDto = CourseTestUtils.GetCourseCreatingDto();
            this.courseMapper = new CourseMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.course = null;
            this.courseDetailsDto = null;
            this.courseCreatingDto = null;
            this.courseMapper = null;
        }

        [TestMethod]
        public void Given_Course_When_CallMap_Then_ShouldReturnCourseDetailsDto()
        {
            // Act
            var result = this.courseMapper.Map(this.course);
            // Assert
            result.Should().BeEquivalentTo(this.courseDetailsDto);
        }

        [TestMethod]
        public void Given_CourseCreatingDto_When_CallMap_Then_ShouldReturnCourse()
        {
            // Act
            var result = this.courseMapper.Map(this.courseCreatingDto);
            // Assert
            result.Should().Match<Course>((obj) =>
                obj.Name == this.course.Name &&
                obj.Year == this.course.Year);
        }

        [TestMethod]
        public void Given_CourseDetailsDtoAndCourse_When_CallMap_Then_ShouldReturnCourse()
        {
            // Arrange
            this.courseDetailsDto.Name = "newCourse";
            // Act
            var result = this.courseMapper.Map(this.courseDetailsDto, this.course);
            // Assert
            result.Should().Match<Course>((obj) =>
                obj.Name == this.courseDetailsDto.Name &&
                obj.Year == this.courseDetailsDto.Year);
        }

        [TestMethod]
        public void Given_CourseCreatingDtoAndId_When_CallMap_Then_ShouldReturnCourseDetailsDto()
        {
            // Act
            var result = this.courseMapper.Map(this.course.Id, this.courseCreatingDto);
            // Assert
            result.Should().BeEquivalentTo(this.courseDetailsDto);
        }
    }
}
