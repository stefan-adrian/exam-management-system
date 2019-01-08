using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Classroom;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Exam.Test.Integration
{
    [TestClass]
    public class ClassroomControllerTest
    {
        private HttpClient client;
        private Classroom classroom1, classroom2;
        private ClassroomDetailsDto classroomDetailsDto1;
        private ClassroomCreatingDto classroomCreatingDto;

        [TestInitialize]
        public void Setup()
        {
            this.client = new CustomWebApplicationFactory<Startup>().CreateClient();
            this.classroom1 = ClassroomTestUtils.GetClassroom();
            this.classroom2 = ClassroomTestUtils.GetClassroom2();
            this.classroomCreatingDto = ClassroomTestUtils.GetClassroomCreatingDto();
            this.classroomDetailsDto1 = ClassroomTestUtils.GetClassroomDetailsDto(classroom1.Id);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.classroom1 = null;
            this.classroom2 = null;
            this.classroomCreatingDto = null;
            this.classroomDetailsDto1 = null;
        }

        [TestMethod]
        public async Task GetAllClassrooms_ShouldReturnAllClassrooms()
        {
            // Arrange
            var expectedResult = new List<ClassroomDetailsDto> {classroomDetailsDto1};
            // Act
            var response = await client.GetAsync("api/classrooms");
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<ClassroomDetailsDto> classroomsDetailsDtoReturned = JsonConvert.DeserializeObject<List<ClassroomDetailsDto>>(responseString);
            classroomsDetailsDtoReturned.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public async Task GetClassroomById_ShouldReturnClassroomWithGivenId()
        {
            // Act
            var response = await client.GetAsync("api/classrooms/" + classroom1.Id);
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var classroomsDetailsDtoReturned = JsonConvert.DeserializeObject<ClassroomDetailsDto>(responseString);
            classroomsDetailsDtoReturned.Should().BeEquivalentTo(classroomDetailsDto1);
        }

        [TestMethod]
        public async Task PostClassroom_ShouldReturnClassroomCreatedFromGivenBody()
        {
            // Arrange
            classroomCreatingDto.Location = "C413";
            var contents = new StringContent(JsonConvert.SerializeObject(classroomCreatingDto), Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("api/classrooms", contents);
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var classroomDetailsDtoReturned = JsonConvert.DeserializeObject<ClassroomDetailsDto>(responseString);
            classroomDetailsDtoReturned.Should().BeEquivalentTo(classroomCreatingDto, options =>
                options.ExcludingMissingMembers());
        }   
    }
}
