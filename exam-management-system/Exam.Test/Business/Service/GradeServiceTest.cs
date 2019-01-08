using Exam.Business.Exam.Service;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Mapper;
using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Moq;
using System.Collections.Generic;
using Exam.Business.Grade.Service;
using Exam.Test.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MockQueryable.NSubstitute;
using FluentAssertions;
using System.Threading.Tasks;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class GradeServiceTest
    {
        private Grade initialStateGrade;
        private GradeDto initialStateGradeDto;
        private GradeCreationDto gradeCreationDto;
        private GradeEditingDto gradeEditingDto;

        //mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IGradeMapper> _mockGradeMapper;
        private Mock<IStudentService> _mockStudentService;
        private Mock<IExamService> _mockExamService;

        //inject mocks
        private GradeService _gradeService;

        [TestInitialize]
        public void Setup()
        {
            this.initialStateGrade = GradeTestUtils.GetInitialStateGrade();
            this.initialStateGradeDto = GradeTestUtils.GetInitialGradeDto(initialStateGrade.Id);
            this.gradeCreationDto = GradeTestUtils.GetGradeCreationDto();
            this.gradeEditingDto = GradeTestUtils.GetGradeEditingDto();
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockGradeMapper = new Mock<IGradeMapper>();
            this._mockStudentService = new Mock<IStudentService>();
            this._mockExamService = new Mock<IExamService>();

            _gradeService = new GradeService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockGradeMapper.Object, _mockStudentService.Object, _mockExamService.Object);
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfGradeDto()
        {
            // Arrange
            _mockExamService.Setup(service => service.GetById(gradeCreationDto.ExamId)).ReturnsAsync(initialStateGrade.Exam);
            _mockStudentService.Setup(service => service.GetStudentById(gradeCreationDto.StudentId))
                .ReturnsAsync(initialStateGrade.Student);
            _mockGradeMapper.Setup(mapper => mapper.Map(gradeCreationDto, initialStateGrade.Student, initialStateGrade.Exam)).Returns(initialStateGrade);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Domain.Entities.Grade>(initialStateGrade))
                .Returns(() => Task.FromResult(initialStateGrade));
            _mockGradeMapper.Setup(mapper => mapper.Map(initialStateGrade)).Returns(initialStateGradeDto);
            // Act
            GradeDto actualGrade = await this._gradeService.Create(gradeCreationDto);
            // Assert
            actualGrade.Should().BeEquivalentTo(initialStateGradeDto);
        }

        [TestMethod]
        public async Task Update_ShouldReturnInstanceOfGradeDto()
        {
            // Arrange
            _mockGradeMapper.Setup(mapper => mapper.Map(initialStateGrade.Id, gradeEditingDto )).Returns(initialStateGradeDto);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Grade>(initialStateGrade.Id)).ReturnsAsync(initialStateGrade);
            // Act
            GradeDto actualGrade = await _gradeService.Update(initialStateGrade.Id, gradeEditingDto);
            // Assert
            actualGrade.Should().BeEquivalentTo(initialStateGradeDto);
        }

        [TestMethod]
        public async Task GetStudentExamGrade_ShouldReturnInstanceOfGradeDto()
        {
            // Arrange
            var expectedGrades = new List<Grade> {initialStateGrade};
            var mockGradesQueryable = expectedGrades.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Grade>()).Returns(mockGradesQueryable);
            _mockGradeMapper.Setup(mapper => mapper.Map(initialStateGrade)).Returns(initialStateGradeDto);
            // Act
            GradeDto actualGrade = await this._gradeService.GetStudentExamGrade(initialStateGrade.Student.Id, initialStateGrade.Exam.Id);
            // Assert
            actualGrade.Should().BeEquivalentTo(initialStateGradeDto);
        }


        [TestMethod]
        public async Task GetAllGradesByExam_ShouldReturnGradesForExamWithThatId()
        {
            // Arrange
            var gradeWithValue = GradeTestUtils.GetGradeWithValue();
            var gradeWithValueDto = GradeTestUtils.GetGradeWithValueDto(gradeWithValue.Id, gradeWithValue.Date);
            var expectedGrades = new List<GradeDto> {gradeWithValueDto};
            var grades = new List<Grade> {gradeWithValue, initialStateGrade};
            var mockGradesQueryable = grades.AsQueryable().BuildMock();
            _mockExamService.Setup(service => service.GetById(gradeWithValue.Exam.Id)).ReturnsAsync(initialStateGrade.Exam);
            _mockReadRepository.Setup(repo => repo.GetAll<Grade>()).Returns(mockGradesQueryable);
            _mockGradeMapper.Setup(mapper => mapper.Map(gradeWithValue)).Returns(gradeWithValueDto);
            _mockGradeMapper.Setup(mapper => mapper.Map(initialStateGrade)).Returns(initialStateGradeDto);
            // Act
            List<GradeDto> actualGrades = await this._gradeService.GetAllGradesByExam(gradeWithValue.Exam.Id);
            // Assert
            actualGrades.Should().BeEquivalentTo(expectedGrades);
        }
    }
}