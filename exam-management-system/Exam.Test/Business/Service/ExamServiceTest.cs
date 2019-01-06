using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Mapper;
using Exam.Business.Exam.Service;
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
    public class ExamServiceTest
    {
        private Domain.Entities.Exam _exam;
        private ExamDto _examDto;
        private ExamCreatingDto _examCreatingDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<ICourseMapper> _mockCourseMapper;
        private Mock<ICourseService> _mockCourseService;
        private Mock<IExamMapper> _mockExamMapper;

        // injectMocks
        private ExamService _examService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._exam = ExamTestUtils.GetExam();
            this._examDto = ExamTestUtils.GetExamDto(_exam.Id);
            this._examCreatingDto = ExamTestUtils.GetExamCreatingDto();
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockCourseMapper = new Mock<ICourseMapper>();
            this._mockCourseService = new Mock<ICourseService>();
            this._mockExamMapper = new Mock<IExamMapper>();
            _examService = new ExamService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockExamMapper.Object, _mockCourseService.Object, _mockCourseMapper.Object);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInstanceOfExamDto()
        {
            // Arrange
            var expectedExams = new List<Domain.Entities.Exam> { _exam };
            var mockExamsQueryable = expectedExams.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Domain.Entities.Exam>()).Returns(mockExamsQueryable);
            _mockExamMapper.Setup(mapper => mapper.Map(_exam)).Returns(_examDto);
            // Act
            ExamDto actualExam = await this._examService.GetById(_exam.Id);
            // Assert
            actualExam.Should().BeEquivalentTo(_examDto);
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfCourseDetailsDto()
        {
            // Arrange
            _mockCourseService.Setup(service => service.GetCourseById(_examCreatingDto.CourseId)).ReturnsAsync(CourseTestUtils.GetCourse());
            _mockExamMapper.Setup(mapper => mapper.Map(_examCreatingDto, CourseTestUtils.GetCourse())).Returns(_exam);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Domain.Entities.Exam>(_exam)).Returns(() => Task.FromResult(_exam));
            _mockExamMapper.Setup(mapper => mapper.Map(_exam)).Returns(_examDto);
            // Act
            ExamDto actualExam = await this._examService.Create(_examCreatingDto);
            // Assert
            actualExam.Should().BeEquivalentTo(_examDto);
        }
    }
}
