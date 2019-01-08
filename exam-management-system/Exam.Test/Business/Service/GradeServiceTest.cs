using Exam.Business.Exam.Service;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Mapper;
using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Exam.Business.Grade.Service;
using Exam.Test.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MockQueryable.NSubstitute;
using FluentAssertions;
using System.Threading.Tasks;
using NSubstitute.ReturnsExtensions;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class GradeServiceTest
    {
        private Grade grade;
        private GradeDto gradeDto;
        private GradeCreationDto gradeCreationDto;

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
            this.grade = GradeTestUtils.GetInitialStateGrade();
            this.gradeDto = GradeTestUtils.GetInitialGradeDto(grade.Id);
            this.gradeCreationDto = GradeTestUtils.GetGradeCreationDto();
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
            _mockExamService.Setup(service => service.GetById(gradeCreationDto.ExamId)).ReturnsAsync(grade.Exam);
            _mockStudentService.Setup(service => service.GetStudentById(gradeCreationDto.StudentId)).ReturnsAsync(grade.Student);
            _mockGradeMapper.Setup(mapper => mapper.Map(gradeCreationDto, grade.Student,grade.Exam)).Returns(grade);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Domain.Entities.Grade>(grade)).Returns(() => Task.FromResult(grade));
            _mockGradeMapper.Setup(mapper => mapper.Map(grade)).Returns(gradeDto);
            // Act
            GradeDto actualGrade = await this._gradeService.Create(gradeCreationDto);
            // Assert
            actualGrade.Should().BeEquivalentTo(gradeDto);
        }

        [TestMethod]
        public async Task GetStudentExamGrade_ShouldReturnInstanceOfGradeDto()
        {
            // Arrange
            var expectedGrades = new List<Domain.Entities.Grade> { grade };
            var mockGradesQueryable = expectedGrades.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Domain.Entities.Grade>()).Returns(mockGradesQueryable);
            _mockGradeMapper.Setup(mapper => mapper.Map(grade)).Returns(gradeDto);
            // Act
            GradeDto actualGrade = await this._gradeService.GetStudentExamGrade(grade.Student.Id,grade.Exam.Id);
            // Assert
            actualGrade.Should().BeEquivalentTo(gradeDto);
        }

    }
}
