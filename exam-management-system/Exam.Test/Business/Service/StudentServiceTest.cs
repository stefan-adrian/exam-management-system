using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Exam.Test.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using MockQueryable.NSubstitute;

namespace Exam.Test.Business.Service
{
    [TestClass]
    public class StudentServiceTest
    {
        private Domain.Entities.Student _student1, _student2;
        private StudentDetailsDto _studentDto1, _studentDto2;
        private StudentCreationDto _studentCreationDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IStudentMapper> _mockStudentMapper;
        // injectMocks
        private StudentService _studentService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._student1 = StudentTestUtils.GetStudent();
            this._student2 = StudentTestUtils.GetStudent();
            this._studentDto1 = StudentTestUtils.GetStudentDetailsDto(_student1.Id);
            this._studentDto2 = StudentTestUtils.GetStudentDetailsDto(_student2.Id);
            this._studentCreationDto = StudentTestUtils.GetStudentCreationDto();
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockStudentMapper = new Mock<IStudentMapper>();
            _studentService = new StudentService(_mockReadRepository.Object, _mockWriteRepository.Object,
                _mockStudentMapper.Object);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            // Arrange
            var expectedStudentsDtoList = new List<StudentDetailsDto> {_studentDto1, _studentDto2};
            var studentsList = new List<Student> {_student1, _student2};
            var mockStudentsQueryable = studentsList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(mockStudentsQueryable);
            _mockStudentMapper.Setup(student => student.Map(_student1)).Returns(_studentDto1);
            _mockStudentMapper.Setup(student => student.Map(_student2)).Returns(_studentDto2);
            // Act
            var actualStudentsDtoList = await _studentService.GetAll();
            // Assert
            actualStudentsDtoList.Should().BeEquivalentTo(expectedStudentsDtoList);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInstanceOfStudentDetailsDto()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Student>(_student1.Id)).ReturnsAsync(_student1);
            _mockStudentMapper.Setup(mapper => mapper.Map(_student1)).Returns(_studentDto1);
            // Act
            StudentDetailsDto actualStudent = await _studentService.GetById(_student1.Id);
            // Assert
            actualStudent.Should().BeEquivalentTo(_studentDto1);
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfStudentDetailsDto()
        {
            // Arrange
            _mockStudentMapper.Setup(mapper => mapper.Map(_student1)).Returns(_studentDto1);
            _mockStudentMapper.Setup(mapper => mapper.Map(_studentCreationDto)).Returns(_student1);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Student>(_student1)).Returns(() => Task.FromResult(_student1));
            // Act
            StudentDetailsDto actualStudent = await _studentService.Create(_studentCreationDto);
            // Assert
            actualStudent.Should().BeEquivalentTo(_studentDto1);
        }

        [TestMethod]
        public async Task Update_ShouldReturnInstanceOfStudentDetailsDto()
        {
            // Arrange
            _mockStudentMapper.Setup(mapper => mapper.Map(_student1.Id,_studentCreationDto)).Returns(_studentDto1);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Student>(_student1.Id)).ReturnsAsync(_student1);
            // Act
            StudentDetailsDto actualStudent = await _studentService.Update(_student1.Id, _studentCreationDto);
            // Assert
            actualStudent.Should().BeEquivalentTo(_studentDto1);
        }

        [TestMethod]
        public void Delete_ShouldRemoveAStudent()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Student>(_student1.Id)).ReturnsAsync(_student1);
            // Act
            Task.Run(async () =>
            {
                await _studentService.Delete(_student1.Id);
            }).GetAwaiter().GetResult();
        }
    }
}