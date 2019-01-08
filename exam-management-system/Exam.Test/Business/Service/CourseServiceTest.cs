using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Exam.Business.Professor;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Test.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using MockQueryable.NSubstitute;
using Exam.Business.Student;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class CourseServiceTest
    {
        private Course _course1, _course2;
        private CourseDto _courseDto1, _courseDto2;
        private CourseCreatingDto _courseCreatingDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<ICourseMapper> _mockCourseMapper;
        private Mock<IProfessorService> _mockProfessorService;
        private Mock<IStudentService> _mockStudentService;
        // injectMocks
        private CourseService _courseService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._course1 = CourseTestUtils.GetCourse();
            this._course2 = CourseTestUtils.GetCourse();
            this._courseDto1 = CourseTestUtils.GetCourseDetailsDto(_course1.Id);
            this._courseDto2 = CourseTestUtils.GetCourseDetailsDto(_course2.Id);
            this._courseCreatingDto = CourseTestUtils.GetCourseCreatingDto();
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockCourseMapper = new Mock<ICourseMapper>();
            _mockProfessorService = new Mock<IProfessorService>();
            _mockStudentService = new Mock<IStudentService>();
            _courseService = new CourseService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockCourseMapper.Object, _mockProfessorService.Object, _mockStudentService.Object);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllSCourses()
        {
            // Arrange
            var expectedCoursesDtoList = new List<CourseDto> { _courseDto1, _courseDto2 };
            var courseList = new List<Course> { _course1, _course2 };
            var mockCoursesQueryable = courseList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Course>()).Returns(mockCoursesQueryable);
            _mockCourseMapper.Setup(course => course.Map(_course1)).Returns(_courseDto1);
            _mockCourseMapper.Setup(course => course.Map(_course2)).Returns(_courseDto2);
            // Act
            var actualCoursesDtoList = await _courseService.GetAll();
            // Assert
            actualCoursesDtoList.Should().BeEquivalentTo(expectedCoursesDtoList);
        }

        [TestMethod]
        public async Task GetAllForProfessor_ShouldReturnAllCourses()
        {
            //Arrange
            Professor professor = ProfessorTestUtils.GetProfessor();
            var expectedCoursesDtoList = new List<CourseDto> { _courseDto1, _courseDto2 };
            var courseList = new List<Course> { _course1, _course2 };
            _mockProfessorService.Setup(professorService =>
                professorService.GetProfessorById(professor.Id)).Returns(() => Task.FromResult(professor));
            var mockCoursesQueryable = courseList.AsQueryable().Where(course => course.Professor == professor).BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Course>()).Returns(mockCoursesQueryable);
            _mockCourseMapper.Setup(course => course.Map(_course1)).Returns(_courseDto1);
            _mockCourseMapper.Setup(course => course.Map(_course2)).Returns(_courseDto2);
            //Act
            var actualCoursesDtoList = await _courseService.GetAllForProfessor(professor.Id);
            //Assert
            actualCoursesDtoList.Should().BeEquivalentTo(expectedCoursesDtoList);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInstanceOfCourseDetailsDto()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course1.Id)).ReturnsAsync(_course1);
            _mockCourseMapper.Setup(mapper => mapper.Map(_course1)).Returns(_courseDto1);
            // Act
            CourseDto actualCourse = await _courseService.GetById(_course1.Id);
            // Assert
            actualCourse.Should().BeEquivalentTo(_courseDto1);
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfCourseDetailsDto()
        {
            // Arrange
            Professor professor = ProfessorTestUtils.GetProfessor();
            _mockCourseMapper.Setup(mapper => mapper.Map(_course1)).Returns(_courseDto1);
            _mockCourseMapper.Setup(mapper => mapper.Map(_courseCreatingDto)).Returns(_course1);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Course>(_course1)).Returns(() => Task.FromResult(_course1));
            _mockProfessorService.Setup(professorService =>
              professorService.GetProfessorById(professor.Id)).Returns(() => Task.FromResult(professor));
            // Act
            CourseDto actualCourse = await _courseService.Create(professor.Id, _courseCreatingDto);
            // Assert
            actualCourse.Should().BeEquivalentTo(_courseDto1);
        }

        [TestMethod]
        public async Task Update_ShouldReturnInstanceOfCourseDetailsDto()
        {
            // Arrange
            _mockCourseMapper.Setup(mapper => mapper.Map(_course1.Id, _courseCreatingDto)).Returns(_courseDto1);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course1.Id)).ReturnsAsync(_course1);
            // Act
            CourseDto actualCourse = await _courseService.Update(_course1.Id, _courseCreatingDto);
            // Assert
            actualCourse.Should().BeEquivalentTo(_courseDto1);
        }

        [TestMethod]
        public void Delete_ShouldRemoveACourse()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course1.Id)).ReturnsAsync(_course1);
            // Act
            Task.Run(async () =>
            {
                await _courseService.Delete(_course1.Id);
            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        public async Task GetAvailableCoursesForStudent_ShouldReturnAllCoursesWithYearLowerOrEqualToStudentYear()
        {
            //Arrange
            Student student = StudentTestUtils.GetStudent();
            _mockStudentService.Setup(studentService => studentService.GetStudentById(student.Id))
                .Returns(() => Task.FromResult(student));
            var expectedCoursesDtoList = new List<CourseDto> { _courseDto1, _courseDto2 };
            var courseList = new List<Course> { _course1, _course2 };
            var mockCoursesQueryable = courseList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Course>()).Returns(mockCoursesQueryable);
            _mockCourseMapper.Setup(course => course.Map(_course1)).Returns(_courseDto1);
            _mockCourseMapper.Setup(course => course.Map(_course2)).Returns(_courseDto2);
            //Act
            var actualCoursesDtoList = await _courseService.GetAvailableCoursesForStudent(student.Id);
            //Assert
            actualCoursesDtoList.Should().BeEquivalentTo(expectedCoursesDtoList);
        }

        [TestMethod]
        public void GetCourseById_ShouldThrowCourseNotFoundException()
        {
            // Arrange
            Guid mockGuid = new Guid();
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course2.Id)).Throws(new CourseNotFoundException(mockGuid));
            // Act
            Func<Task> act = async () => await _courseService.GetCourseById(mockGuid);
            // Assert
            act.Should().Throw<CourseNotFoundException>();
        }
    }
}

