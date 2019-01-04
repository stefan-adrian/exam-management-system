using Exam.Business.StudentCourse;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mapper
{
    [TestClass]
    public class StudentCourseMapperTests
    {
        private StudentCourse _studentCourse;
        private StudentCourseDetailsDto _studentCourseDetailsDto;
        private StudentCourseCreationDto _studentCourseCreationDto;
        private IStudentCourseMapper _studentCourseMapper;

        [TestInitialize]
        public void Setup()
        {
            this._studentCourse = StudentCourseTestUtils.GetStudentCourse();
            this._studentCourseCreationDto =
                StudentCourseTestUtils.GetStudentCourseCreationDto(this._studentCourse.CourseId);
            this._studentCourseDetailsDto =
                StudentCourseTestUtils.GetStudentCourseDetailsDto(this._studentCourse.StudentId,
                    this._studentCourse.CourseId);
            this._studentCourseMapper = new StudentCourseMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._studentCourse = null;
            this._studentCourseCreationDto = null;
            this._studentCourseDetailsDto = null;
            this._studentCourseMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnStudentCourse_WhenArgumentsAreStudentIdAndStudentCourseCreationDto()
        {
            // Act
            var result = this._studentCourseMapper.Map(this._studentCourse.StudentId, this._studentCourseCreationDto);
            // Assert
            result.Should().Match<StudentCourse>((obj) =>
                obj.CourseId == this._studentCourse.CourseId &&
                obj.StudentId == this._studentCourse.StudentId);
        }

    }
}
