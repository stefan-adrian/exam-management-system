using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Exam.Business.Student.Exception;
using Exam.Business.StudentCourse;
using Exam.Business.StudentCourse.Exception;
using Exam.Business.StudentCourse.Service;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Test.TestUtils;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.NSubstitute;
using Moq;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class StudentCourseServiceTest
    {
        private StudentCourse _studentCourse1, _studentCourse2;
        private StudentCourseDetailsDto _studentCourseDto1, _studentCourseDto2;
        private StudentCourseCreationDto _studentCourseCreationDto;
        private Student _student;
        private Course _course;
        private CourseDto _courseDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IStudentCourseMapper> _mockStudentCourseMapper;
        private Mock<ICourseMapper> _mockCourseMapper;
        // injectMocks
        private StudentCourseService _studentCourseService;

        [TestInitialize]
        public void Setup()
        {
            this._student = StudentTestUtils.GetStudent();
            this._course = CourseTestUtils.GetCourse();
            this._courseDto = CourseTestUtils.GetCourseDetailsDto(_course.Id);
            this._studentCourse1 = StudentCourseTestUtils.GetStudentCourse(_student.Id, _course.Id);
            this._studentCourse2 = StudentCourseTestUtils.GetStudentCourse2();
            this._studentCourseCreationDto =
                StudentCourseTestUtils.GetStudentCourseCreationDto(this._studentCourse1.CourseId);
            this._studentCourseDto1 =
                StudentCourseTestUtils.GetStudentCourseDetailsDto(this._studentCourse1.StudentId,
                    this._studentCourse1.CourseId);
            this._studentCourseDto2 =
                StudentCourseTestUtils.GetStudentCourseDetailsDto(this._studentCourse2.StudentId,
                    this._studentCourse2.CourseId);
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockStudentCourseMapper = new Mock<IStudentCourseMapper>();
            this._mockCourseMapper = new Mock<ICourseMapper>();
            this._studentCourseService = new StudentCourseService(_mockReadRepository.Object, _mockWriteRepository.Object, _mockStudentCourseMapper.Object, _mockCourseMapper.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._studentCourse1 = null;
            this._studentCourse2 = null;
            this._studentCourseDto1 = null;
            this._studentCourseDto2 = null;
            this._studentCourseCreationDto = null;
            this._mockReadRepository = null;
            this._mockWriteRepository = null;
            this._mockStudentCourseMapper = null;
            this._mockCourseMapper = null;
            this._studentCourseService = null;
        }

        [TestMethod]
        public async Task AddCourse_ShouldReturnInstanceOfStudentCourseDetailsDto()
        {
            // Arrange
            var expectedStudents = new List<Student> {_student};
            var mockStudentQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentQueryable);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course.Id))
                .ReturnsAsync(_course);
            _mockStudentCourseMapper.Setup(mapper => mapper.Map(_student.Id, _studentCourseCreationDto)).Returns(_studentCourse1);
            _mockStudentCourseMapper.Setup(mapper => mapper.Map(_studentCourse1)).Returns(_studentCourseDto1);
            _mockWriteRepository.Setup(repo =>repo.AddNewAsync<StudentCourse>(_studentCourse1)).Returns(() => Task.FromResult(_studentCourse1));
            // Act
            var actualStudentCourse =
                await _studentCourseService.AddCourse(_student.Id, _studentCourseCreationDto);
            // Assert
            actualStudentCourse.Should().BeEquivalentTo(_studentCourseDto1);
        }

        [TestMethod]
        [ExpectedException(typeof(StudentNotFoundException))]
        public async Task AddCourse_ShouldThrowStudentNotFoundException_WhenStudentIsNull()
        {
            // Arrange
            var expectedStudents = new List<Student>();
            var mockStudentQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentQueryable);
            // Act
            var actualStudentCourse = await _studentCourseService.AddCourse(_student.Id, _studentCourseCreationDto);
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public async Task AddCourse_ShouldThrowCourseNotFoundException_WhenCourseIsNull()
        {
            // Arrange
            var expectedStudents = new List<Student> { _student };
            var mockStudentQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentQueryable);
            Course nullCourse = null;
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course.Id))
                .ReturnsAsync(nullCourse);
            // Act
            var actualStudentCourse = await _studentCourseService.AddCourse(_student.Id, _studentCourseCreationDto);
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(StudentAlreadyAppliedException))]
        public async Task AddCourse_ShouldThrowStudentAlreadyAppliedException_WhenStudentAlreadyAppliedToCourse()
        {
            // Arrange
            var expectedStudents = new List<Student> { _student };
            var mockStudentQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentQueryable);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Course>(_course.Id))
                .ReturnsAsync(_course);
            _mockStudentCourseMapper.Setup(mapper => mapper.Map(_student.Id, _studentCourseCreationDto)).Throws(new StudentAlreadyAppliedException(_student.Id, _course.Id));

            // Act
            var actualStudentCourse =
                await _studentCourseService.AddCourse(_student.Id, _studentCourseCreationDto);
            // Assert
        }

        [TestMethod]
        public async Task GetCourses_ShouldReturnAllCoursesThatStudentHasApplied()
        {
            // Arrange
            var expectedCourseDtoList = new List<CourseDto> { _courseDto };
            var expectedStudents = new List<Student> { _student };
            var mockStudentsQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentsQueryable);
            _mockCourseMapper.Setup(mapper => mapper.Map(_student.StudentCourses.ToList())).Returns(expectedCourseDtoList);
            // Act
            var result = await this._studentCourseService.GetCourses(_student.Id);
            // Assert
            result.IsSameOrEqualTo(expectedCourseDtoList);
        }

        [TestMethod]
        [ExpectedException(typeof(StudentNotFoundException))]
        public async Task GetCourses_ShouldThrowStudentNotFoundException_WhenStudentIsNull()
        {
            // Arrange
            var expectedStudents = new List<Student>();
            var mockStudentsQueryable = expectedStudents.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentsQueryable);
            // Act
            var result = await this._studentCourseService.GetCourses(_student.Id);
            // Assert
        }
    }
}
