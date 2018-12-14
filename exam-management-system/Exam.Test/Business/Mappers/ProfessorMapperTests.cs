using Exam.Business.Professor;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mappers
{
    [TestClass]
    public class ProfessorMapperTests
    {
        private Professor _professor;
        private ProfessorDetailsDto _professorDetailsDto;
        private ProfessorCreatingDto _professorCreatingDto;
        private IProfessorMapper _professorMapper;

        [TestInitialize]
        public void Setup()
        {
            this._professor = ProfessorTestUtils.GetProfessor();
            this._professorDetailsDto = ProfessorTestUtils.GetProfessorDetailsDto(_professor.Id);
            this._professorCreatingDto = ProfessorTestUtils.GetProfessorCreatingDto();
            this._professorMapper = new ProfessorMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._professor = null;
            this._professorDetailsDto = null;
            this._professorCreatingDto = null;
            this._professorMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnProfessorDetailsDto_WhenArgumentIsProfessor()
        { 
            // Act
            var result = _professorMapper.Map(this._professor);
            // Assert
            result.Should().BeEquivalentTo(this._professorDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnProfessorsDetails_WhenArgumentsAreProfessorCreatingDtoAndId()
        {
            // Act
            var result = this._professorMapper.Map(this._professor.Id, this._professorCreatingDto);
            // Assert
            result.Should().BeEquivalentTo(this._professorDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnProfessor_WhenArgumentIsProfessorCreatingDto()
        {
            // Act
            var result = this._professorMapper.Map(this._professorCreatingDto);
            // Assert
            result.Should().Match<Professor>((obj) =>
                obj.RegistrationNumber == this._professor.RegistrationNumber &&
                obj.Email == this._professor.Email &&
                obj.FirstName == this._professor.FirstName &&
                obj.LastName == this._professor.LastName &&
                obj.Password == this._professor.Password);
        }

        [TestMethod]
        public void Map_ShouldReturnProfessor_WhenArgumentsAreProfessorDetailsDtoAndProfessor()
        {
            // Arrange
            this._professorDetailsDto.Email = "newEmail";
            // Act
            var result = this._professorMapper.Map(this._professorDetailsDto, this._professor);
            // Assert
            result.Should().Match<Professor>((obj) =>
                obj.Email == this._professorDetailsDto.Email);
        }
    }
}
