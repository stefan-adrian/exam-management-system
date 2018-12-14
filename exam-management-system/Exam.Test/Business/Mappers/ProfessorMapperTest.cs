using Exam.Business.Professor;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mappers
{
    [TestClass]
    public class ProfessorMapperTest
    {
        private Professor professor;
        private ProfessorDetailsDto professorDetailsDto;
        private ProfessorCreatingDto professorCreatingDto;
        private IProfessorMapper professorMapper;

        [TestInitialize]
        public void Setup()
        {
            this.professor = ProfessorTestUtils.GetProfessor();
            this.professorDetailsDto = ProfessorTestUtils.GetProfessorDetailsDto(professor.Id);
            this.professorCreatingDto = ProfessorTestUtils.GetProfessorCreatingDto();
            this.professorMapper = new ProfessorMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.professor = null;
            this.professorDetailsDto = null;
            this.professorCreatingDto = null;
            this.professorMapper = null;
        }

        [TestMethod]
        public void Given_Professor_When_CallMap_Then_ShouldReturnProfessorDetailsDto()
        {
            // Arrange

            // Act
            var result = professorMapper.Map(this.professor);
            // Assert
            result.Should().BeEquivalentTo(this.professorDetailsDto);
        }

        [TestMethod]
        public void Given_ProfessorCreatingDtoAndId_When_CallMap_Then_ShouldReturnProfessorDetails()
        {
            // Arrange

            // Act
            var result = this.professorMapper.Map(this.professor.Id, this.professorCreatingDto);
            // Assert
            result.Should().BeEquivalentTo(this.professorDetailsDto);
        }

        [TestMethod]
        public void Given_ProfessorCreatingDto_When_CallMap_Then_ShouldReturnProfessor()
        {
            // Arrange

            // Act
            var result = this.professorMapper.Map(this.professorCreatingDto);
            // Assert
            result.Should().Match<Professor>((obj) =>
                obj.RegistrationNumber == this.professor.RegistrationNumber &&
                obj.Email == this.professor.Email &&
                obj.FirstName == this.professor.FirstName &&
                obj.LastName == this.professor.LastName &&
                obj.Password == this.professor.Password);
        }

        [TestMethod]
        public void Given_ProfessorDetailsDtoAndProfessor_When_CallMap_Then_ShouldReturnProfessor()
        {
            // Arrange
            this.professorDetailsDto.Email = "newEmail";
            // Act
            var result = this.professorMapper.Map(this.professorDetailsDto, this.professor);
            // Assert
            result.Should().Match<Professor>((obj) =>
                obj.RegistrationNumber == this.professorDetailsDto.RegistrationNumber &&
                obj.Email == this.professorDetailsDto.Email &&
                obj.FirstName == this.professorDetailsDto.FirstName &&
                obj.LastName == this.professorDetailsDto.LastName &&
                obj.Password == this.professorDetailsDto.Password);
        }
    }
}
