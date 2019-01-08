using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Exam.Dto;
using Exam.Business.Exam.Exception;
using Exam.Business.Exam.Mapper;
using Exam.Business.Exam.Service;
using Exam.Business.Student;
using Exam.Business.StudentCourse.Exception;
using Exam.Business.StudentCourse.Service;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.NSubstitute;
using Moq;
using NSubstitute;

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
        private Mock<ICourseService> _mockCourseService;
        private Mock<IExamMapper> _mockExamMapper;
        private Mock<IStudentCourseService> _mockStudentCourseService;
        private Mock<IStudentService> _mockStudentService;

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
            this._mockCourseService = new Mock<ICourseService>();
            this._mockExamMapper = new Mock<IExamMapper>();
            this._mockStudentCourseService = new Mock<IStudentCourseService>();
            this._mockStudentService = new Mock<IStudentService>();
            _examService = new ExamService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockExamMapper.Object, _mockCourseService.Object,
                _mockStudentCourseService.Object, _mockStudentService.Object);
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
            ExamDto actualExam = await this._examService.GetDtoById(_exam.Id);
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

        [TestMethod]
        public void GetExamByIdFetchingCourse_ShouldThrowExamNotFoundException()
        {
            // Arrange
            Guid mockGuid = new Guid();
            var expectedExams = new List<Domain.Entities.Exam> { _exam };
            var mockExamsQueryable = expectedExams.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Domain.Entities.Exam>()).Throws(new ExamNotFoundException(mockGuid));
            // Act
            Func<Task> act = async () => await _examService.GetByIdFetchingCourse(mockGuid);
            // Assert
            act.Should().Throw<ExamNotFoundException>();
        }

        [TestMethod]
        public void GetExamById_ShouldThrowExamNotFoundException()
        {
            // Arrange
            Guid mockGuid = new Guid();
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Domain.Entities.Exam>(mockGuid)).Throws(new ExamNotFoundException(mockGuid));
            // Act
            Func<Task> act = async () => await _examService.GetById(mockGuid);
            // Assert
            act.Should().Throw<ExamNotFoundException>();
        }

        [TestMethod]
        public async Task GetExamById_ShouldReturnExamWithThatId()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Domain.Entities.Exam>(_exam.Id)).ReturnsAsync(_exam);
            // Act
            Domain.Entities.Exam actualExam = await this._examService.GetById(_exam.Id);
            // Assert
            actualExam.Should().BeEquivalentTo(_exam);
        }

        [TestMethod]
        public async Task GetAllExamsFromCourseForStudent_ShouldReturnExamsForStudentWithThatId()
        {
            // Arrange
            var expectedExamsDtoList = new List<ExamDto> { _examDto };
            var course = _exam.Course;
            var courseDtoList = new List<CourseDto> { CourseTestUtils.GetCourseDetailsDto(CourseTestUtils.GetCourse().Id) };
            var student = StudentTestUtils.GetStudent();
            _mockCourseService.Setup(service => service.GetCourseById(course.Id)).ReturnsAsync(course);
            _mockStudentCourseService.Setup(service => service.GetCourses(student.Id)).ReturnsAsync(courseDtoList);
            _mockStudentService.Setup(service => service.GetStudentById(student.Id)).ReturnsAsync(student);
            var examsList = new List<Domain.Entities.Exam> { ExamTestUtils.GetExam() };
            var mockExamsQueryable = examsList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Domain.Entities.Exam>()).Returns(mockExamsQueryable);
            _mockExamMapper.Setup(mapper => mapper.Map(_exam)).Returns(_examDto);
            // Act
            var actualExamsDtoList = await _examService.GetAllExamsFromCourseForStudent(course.Id, student.Id);
            // Assert
            actualExamsDtoList.Should().BeEquivalentTo(expectedExamsDtoList);
        }

        [TestMethod]
        public async Task GetAllExamsFromCourseForStudent_ShouldThrowStudentNotAppliedToCourse()
        {
            // Arrange
            var course = _exam.Course;
            var emptyCourseDtoList = new List<CourseDto>();
            var student = StudentTestUtils.GetStudent();
            _mockCourseService.Setup(service => service.GetCourseById(course.Id)).ReturnsAsync(course);
            _mockStudentCourseService.Setup(service => service.GetCourses(student.Id)).ReturnsAsync(emptyCourseDtoList);
            _mockStudentService.Setup(service => service.GetStudentById(student.Id)).ReturnsAsync(student);
            // Act
            Func<Task> act = async () => await _examService.GetAllExamsFromCourseForStudent(course.Id, student.Id);
            // Assert
            act.Should().Throw<StudentNotAppliedToCourse>(student.Id.ToString(), course.Id.ToString());
        }

        [TestMethod]
        public async Task getAllExamsForACourse_ShouldReturnListOfExamDto()
        {
            var course = new Course(_exam.Course.Name, _exam.Course.Year, new List<Domain.Entities.Exam> {_exam});
            var courses = new List<Course> {course};
            var exams = new List<ExamDto> { _examDto};
            var mockExamsQueryable = courses.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Domain.Entities.Course>()).Returns(mockExamsQueryable);
            _mockExamMapper.Setup(mapper => mapper.Map(_exam)).Returns(_examDto);
            var actualExamsDtoList = await _examService.GetAllExamsForACourse(course.Id);
            // Assert
            actualExamsDtoList.Should().BeEquivalentTo(exams);
        }
    }
}
