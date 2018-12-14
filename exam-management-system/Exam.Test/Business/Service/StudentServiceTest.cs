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


namespace Exam.Test.Business.Service
{
    [TestClass]
    public class StudentServiceTest
    {
        private Domain.Entities.Student _student1, _student2;
        private StudentDetailsDto _studentDto1, _studentDto2;
        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IStudentMapper> _mockStudentMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            this._student1 = StudentTestUtils.GetStudent();
            this._student2 = StudentTestUtils.GetStudent();
            this._studentDto1 = StudentTestUtils.GetStudentDetailsDto(_student1.Id);
            this._studentDto2 = StudentTestUtils.GetStudentDetailsDto(_student2.Id);
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockStudentMapper = new Mock<IStudentMapper>();

        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllStudents()
        {
            // Arrange
            var expectedStudentsDtoList = new List<StudentDetailsDto> { _studentDto1, _studentDto2 };
            var expectedStudentsList = new List<Student> { _student1, _student2 };
            var expectedStudents = expectedStudentsList.AsQueryable();
            var studentService = new StudentService(_mockReadRepository.Object, _mockWriteRepository.Object, _mockStudentMapper.Object);
            _mockReadRepository.Setup(repo => repo.GetAll<Student>()).Returns(expectedStudents);
            _mockStudentMapper.Setup(student => student.Map(_student1)).Returns(_studentDto1);
            _mockStudentMapper.Setup(student => student.Map(_student2)).Returns(_studentDto1);

            // Act
            List<StudentDetailsDto> actualStudentsDtoList = await studentService.GetAll();

            // Assert
            actualStudentsDtoList.Should().BeEquivalentTo(expectedStudentsDtoList);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInstanceOfStudentDetailsDto()
        {
            // Arrange
            var studentService = new StudentService(_mockReadRepository.Object, _mockWriteRepository.Object, _mockStudentMapper.Object);
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Student>(_student1.Id)).ReturnsAsync(_student1);
            _mockStudentMapper.Setup(student => student.Map(_student1)).Returns(_studentDto1);

            // Act
            StudentDetailsDto actualStudent = await studentService.GetById(_student1.Id);

            // Assert
            actualStudent.Should().BeEquivalentTo(_studentDto1);
        }
    }
}