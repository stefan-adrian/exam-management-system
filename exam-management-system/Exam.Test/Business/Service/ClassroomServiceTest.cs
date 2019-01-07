using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Classroom;
using Exam.Business.Classroom.Exception;
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
    public class ClassroomServiceTest
    {
        private Classroom _classroom1, _classroom2;
        private ClassroomDetailsDto _classroomDetailsDto1, _classroomDetailsDto2;
        private ClassroomCreatingDto _classroomCreatingDto;

        // mocks
        private Mock<IReadRepository> _mockReadRepository;
        private Mock<IWriteRepository> _mockWriteRepository;
        private Mock<IClassroomMapper> _mockClassroomMapper;
        //injectMocks
        private ClassroomService _classroomService;

        [TestInitialize]
        public void Setup()
        {
            this._classroom1 = ClassroomTestUtils.GetClassroom();
            this._classroom2 = ClassroomTestUtils.GetClassroom2();
            this._classroomCreatingDto = ClassroomTestUtils.GetClassroomCreatingDto();
            this._classroomDetailsDto1 = ClassroomTestUtils.GetClassroomDetailsDto(_classroom1.Id);
            this._classroomDetailsDto2 = ClassroomTestUtils.GetClassroomDetailsDto(_classroom2.Id);
            this._mockReadRepository = new Mock<IReadRepository>();
            this._mockWriteRepository = new Mock<IWriteRepository>();
            this._mockClassroomMapper = new Mock<IClassroomMapper>();
            this._classroomService = new ClassroomService(_mockWriteRepository.Object, _mockReadRepository.Object, _mockClassroomMapper.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._classroom1 = null;
            this._classroom2 = null;
            this._classroomCreatingDto = null;
            this._classroomDetailsDto1 = null;
            this._classroomDetailsDto2 = null;
            this._mockReadRepository = null;
            this._mockWriteRepository = null;
            this._mockClassroomMapper = null;
            this._classroomService = null;
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllClassrooms()
        {
            // Arrange
            var expectedClassroomDtoList = new List<ClassroomDetailsDto> {_classroomDetailsDto1, _classroomDetailsDto2};
            var classroomList = new List<Classroom>{ _classroom1, _classroom2};
            var mockClassroomQueryable = classroomList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Classroom>()).Returns(mockClassroomQueryable);
            _mockClassroomMapper.Setup(classroom => classroom.Map(_classroom1)).Returns(_classroomDetailsDto1);
            _mockClassroomMapper.Setup(classroom => classroom.Map(_classroom2)).Returns(_classroomDetailsDto2);
            // Act
            var actualClassroomDtoList = await _classroomService.GetAll();
            // Assert
            actualClassroomDtoList.Should().BeEquivalentTo(expectedClassroomDtoList);
        }

        [TestMethod]
        public async Task GetDetailsDtoById_ShouldReturnInstanceOfClassroomDetailsDto()
        {
            // Arrange
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Classroom>(_classroom1.Id)).ReturnsAsync(_classroom1);
            _mockClassroomMapper.Setup(mapper => mapper.Map(_classroom1)).Returns(_classroomDetailsDto1);
            // Act
            var actualClassroom = await _classroomService.GetDetailsDtoById(_classroom1.Id);
            // Assert
            actualClassroom.Should().BeEquivalentTo(_classroomDetailsDto1);
        }

        [TestMethod]
        [ExpectedException(typeof(ClassroomNotFoundException))]
        public async Task GetDetailsDtoById_ShouldThrowClassroomNotFoundException_WhenClassroomIsNull()
        {
            // Arrange
            Classroom nullClassroom = null;
            _mockReadRepository.Setup(repo => repo.GetByIdAsync<Classroom>(_classroom1.Id)).ReturnsAsync(nullClassroom);
            _mockClassroomMapper.Setup(mapper => mapper.Map(_classroom1)).Returns(_classroomDetailsDto1);
            // Act
            var actualClassroom = await _classroomService.GetDetailsDtoById(_classroom1.Id);
            // Assert
        }

        [TestMethod]
        public async Task Create_ShouldReturnInstanceOfClassroomDetailsDto()
        {
            // Arrange
            var classroomList = new List<Classroom>{};
            var mockClassroomQueryable = classroomList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Classroom>()).Returns(mockClassroomQueryable);
            _mockClassroomMapper.Setup(mapper => mapper.Map(_classroomCreatingDto)).Returns(_classroom1);
            _mockClassroomMapper.Setup(mapper => mapper.Map(_classroom1)).Returns(_classroomDetailsDto1);
            _mockWriteRepository.Setup(repo => repo.AddNewAsync<Classroom>(_classroom1))
                .Returns(() => Task.FromResult(_classroom1));
            // Act
            var actualClassroom = await _classroomService.Create(_classroomCreatingDto);
            // Assert
            actualClassroom.Should().BeEquivalentTo(_classroomDetailsDto1);
        }

        [TestMethod]
        [ExpectedException(typeof(ClassroomLocationAlreadyExistsException))]
        public async Task Create_ShouldThrowClassroomLocationAlreadyExistsException_WhenClassroomLocationAlreadyExists()
        {
            // Arrange
            var classroomList = new List<Classroom> { _classroom1, _classroom2 };
            var mockClassroomQueryable = classroomList.AsQueryable().BuildMock();
            _mockReadRepository.Setup(repo => repo.GetAll<Classroom>()).Returns(mockClassroomQueryable);
            _mockClassroomMapper.Setup(classroom => classroom.Map(_classroom1)).Returns(_classroomDetailsDto1);
            _mockClassroomMapper.Setup(classroom => classroom.Map(_classroom2)).Returns(_classroomDetailsDto2);
            // Act
            await _classroomService.Create(_classroomCreatingDto);
            // Assert
        }
    }
}
