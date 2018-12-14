using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Professor;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.NSubstitute;
using Moq;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class ProfessorServiceTest
    {
        private Domain.Entities.Professor _professor1, _professor2;
        private ProfessorDetailsDto _professorDetailsDto1, _professorDetailsDto2;
        private ProfessorCreatingDto _professorCreatingDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IProfessorMapper> _mockProfessorMapper;
        // injectMocks
        private ProfessorService _professorService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._professor1 = ProfessorTestUtils.GetProfessor();
            this._professor2 = ProfessorTestUtils.GetProfessor();
            this._professorDetailsDto1 = ProfessorTestUtils.GetProfessorDetailsDto(_professor1.Id);
            this._professorDetailsDto2 = ProfessorTestUtils.GetProfessorDetailsDto(_professor2.Id);
            this._professorCreatingDto = ProfessorTestUtils.GetProfessorCreatingDto();
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockProfessorMapper = new Mock<IProfessorMapper>();
            _professorService = new ProfessorService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockProfessorMapper.Object);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllProfessors()
        {
            // Arrange
            var expectedProfessorsDtoList = new List<ProfessorDetailsDto> { _professorDetailsDto1, _professorDetailsDto2 };
            var professorsList = new List<Professor> { _professor1, _professor2 };
            var mockProfessorsQueryable = professorsList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Professor>()).Returns(mockProfessorsQueryable);
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professor1)).Returns(_professorDetailsDto1);
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professor2)).Returns(_professorDetailsDto2);
            // Act
            var actualProfessorDtoList = await _professorService.GetAll();
            // Assert
            actualProfessorDtoList.Should().BeEquivalentTo(expectedProfessorsDtoList);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInstanceOfProfessorDetailsDto()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Professor>(_professor1.Id)).ReturnsAsync(_professor1);
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professor1)).Returns(_professorDetailsDto1);
            // Act
            ProfessorDetailsDto actualProfessor = await _professorService.GetById(_professor1.Id);
            // Assert
            actualProfessor.Should().BeEquivalentTo(_professorDetailsDto1);
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfProfessorDetailsDto()
        {
            // Arrange
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professor1)).Returns(_professorDetailsDto1);
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professorCreatingDto)).Returns(_professor1);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Professor>(_professor1)).Returns(() => Task.FromResult(_professor1));
            // Act
            ProfessorDetailsDto actualProfessor = await _professorService.Create(_professorCreatingDto);
            // Assert
            actualProfessor.Should().BeEquivalentTo(_professorDetailsDto1);
        }

        [TestMethod]
        public async Task Update_ShouldReturnInstanceOfProfessorDetailsDto()
        {
            // Arrange
            _mockProfessorMapper.Setup(mapper => mapper.Map(_professor1.Id, _professorCreatingDto)).Returns(_professorDetailsDto1);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Professor>(_professor1.Id)).ReturnsAsync(_professor1);
            // Act
            ProfessorDetailsDto actualProfessor = await _professorService.Update(_professor1.Id, _professorCreatingDto);
            // Assert
            actualProfessor.Should().BeEquivalentTo(_professorDetailsDto1);
        }

        [TestMethod]
        public void Delete_ShouldRemoveAProfessor()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Professor>(_professor1.Id)).ReturnsAsync(_professor1);
            // Act
            Task.Run(async () =>
            {
                await _professorService.Delete(_professor1.Id);
            }).GetAwaiter().GetResult();
        }
    }
}
