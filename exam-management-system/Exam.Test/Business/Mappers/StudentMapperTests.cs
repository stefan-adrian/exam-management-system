using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mappers
{
    [TestClass]
    public class StudentMapperTests
    {
        private Student _student;
        private StudentDetailsDto _studentDetailsDto;
        private StudentCreationDto _studentCreationDto;
        private IStudentMapper _studentMapper;

        [TestInitialize]
        public void Setup()
        {
            this._student = StudentTestUtils.GetStudent();
            this._studentDetailsDto = StudentTestUtils.GetStudentDetailsDto(this._student.Id);
            this._studentCreationDto = StudentTestUtils.GetStudentCreationDto();
            this._studentMapper = new StudentMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._student = null;
            this._studentDetailsDto = null;
            this._studentCreationDto = null;
            this._studentMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnStudent_WhenArgumentIsStudentCreationDto()
        {
            // Act
            var result = this._studentMapper.Map(this._studentCreationDto);
            // Assert
            result.Should().Match<Student>((obj) => 
                obj.Email == this._student.Email &&
                obj.FirstName == this._student.FirstName &&
                obj.LastName == this._student.LastName &&
                obj.Password == this._student.Password &&
                obj.RegistrationNumber == this._student.RegistrationNumber &&
                obj.YearOfStudy == this._student.YearOfStudy);
        }

        [TestMethod]
        public void Map_ShouldReturnStudentDetailsDto_WhenArgumentIsStudent()
        {
            // Act
            var result = this._studentMapper.Map(this._student);
            // Assert
            result.Should().BeEquivalentTo(this._studentDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnStudentDetailsDto_WhenArgumentsAreStudentCreationDtoAndId()
        {
            // Act
            var result = this._studentMapper.Map(this._student.Id, this._studentCreationDto);
            // Assert
            result.Should().BeEquivalentTo(this._studentDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnStudent_WhenArgumentsAreStudentDetailsDtoAndStudent()
        {
            // Arrange
            this._studentDetailsDto.FirstName = "firstNameChanged";
            // Act
            var result = this._studentMapper.Map(this._studentDetailsDto, this._student);
            // Assert
            result.Should().Match<Student>((obj) =>
                obj.FirstName == this._studentDetailsDto.FirstName);
        }
    }
}
