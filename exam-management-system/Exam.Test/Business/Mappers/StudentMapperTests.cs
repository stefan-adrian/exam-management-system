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
        private Student student;
        private StudentDetailsDto studentDetailsDto;
        private StudentCreationDto studentCreationDto;
        private IStudentMapper studentMapper;

        [TestInitialize]
        public void Setup()
        {
            this.student = StudentTestUtils.GetStudent();
            this.studentDetailsDto = StudentTestUtils.GetStudentDetailsDto(this.student.Id);
            this.studentCreationDto = StudentTestUtils.GetStudentCreationDto();
            this.studentMapper = new StudentMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.student = null;
            this.studentDetailsDto = null;
            this.studentCreationDto = null;
            this.studentMapper = null;
        }

        [TestMethod]
        public void Given_StudentCreationDto_When_CallMap_Then_ShouldReturnStudent()
        {
            // Act
            var result = this.studentMapper.Map(this.studentCreationDto);
            // Assert
            result.Should().Match<Student>((obj) => 
                obj.Email == this.student.Email &&
                obj.FirstName == this.student.FirstName &&
                obj.LastName == this.student.LastName &&
                obj.Password == this.student.Password &&
                obj.RegistrationNumber == this.student.RegistrationNumber &&
                obj.YearOfStudy == this.student.YearOfStudy);
        }

        [TestMethod]
        public void Given_Student_When_CallMap_Then_ShouldReturnStudentDetailsDto()
        {
            // Act
            var result = this.studentMapper.Map(this.student);
            // Assert
            result.Should().BeEquivalentTo(this.studentDetailsDto);
        }

        [TestMethod]
        public void Given_StudentCreationDtoAndId_When_CallMap_Then_ShouldReturnStudentDetailsDto()
        {
            // Act
            var result = this.studentMapper.Map(this.student.Id, this.studentCreationDto);
            // Assert
            result.Should().BeEquivalentTo(this.studentDetailsDto);
        }

        [TestMethod]
        public void Given_StudentDetailsDtoAndStudent_When_CallMap_Then_ShouldReturnStudent()
        {
            // Arrange
            this.studentDetailsDto.FirstName = "firstNameChanged";
            // Act
            var result = this.studentMapper.Map(this.studentDetailsDto, this.student);
            // Assert
            result.Should().Match<Student>((obj) =>
                obj.Email == this.studentDetailsDto.Email &&
                obj.FirstName == this.studentDetailsDto.FirstName &&
                obj.LastName == this.studentDetailsDto.LastName &&
                obj.Password == this.studentDetailsDto.Password &&
                obj.RegistrationNumber == this.studentDetailsDto.RegistrationNumber &&
                obj.YearOfStudy == this.studentDetailsDto.YearOfStudy);
        }
    }
}
