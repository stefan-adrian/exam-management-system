using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;

namespace Exam.Test.Integration
{
    [TestClass]
    public class StudentControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private Student student1;
        private Student student2;
        private StudentDetailsDto studentDetailsDto1;
        private StudentDetailsDto studentDetailsDto2;
        private StudentCreationDto studentCreationDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            student1 = StudentTestUtils.GetStudent();
            student2 = StudentTestUtils.GetStudent2();
            studentDetailsDto1 = StudentTestUtils.GetStudentDetailsDto(student1.Id);
            studentDetailsDto2 = StudentTestUtils.GetStudentDetailsDto(student2.Id);
            studentCreationDto = StudentTestUtils.GetStudentCreationDto();
        }

        [TestMethod]
        public async Task GetStudentById_ShouldReturnStudentWithGivenId()
        {

            //Act
            var response = await client.GetAsync("api/students/" + student1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            StudentDetailsDto studentDetailsDtoReturned = JsonConvert.DeserializeObject<StudentDetailsDto>(responseString);
            studentDetailsDtoReturned.Should().BeEquivalentTo(studentDetailsDto1);
        }

        [TestMethod]
        public async Task GetAllStudents_ShouldReturnAllStudents()
        {
            //Arrange
            List<StudentDetailsDto> studentDetailsDtos = new List<StudentDetailsDto>();
            studentDetailsDtos.Add(studentDetailsDto1);
            studentDetailsDtos.Add(studentDetailsDto2);

            //Act
            var response = await client.GetAsync("api/students");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<StudentDetailsDto> studentsDetailsDtosReturned = JsonConvert.DeserializeObject<List<StudentDetailsDto>>(responseString);
            studentsDetailsDtosReturned.Should().BeEquivalentTo(studentDetailsDtos);
        }

        [TestMethod]
        public async Task PostStudent_ShouldReturnStudentCreatedFromGivenBody()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(studentCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/students", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            StudentDetailsDto studentsDetailsDtoReturned = JsonConvert.DeserializeObject<StudentDetailsDto>(responseString);
            studentsDetailsDtoReturned.Should().BeEquivalentTo(studentCreationDto, options =>
                 options.ExcludingMissingMembers());

        }

        [TestMethod]
        public async Task PutStudentById_ShouldHaveSuccessStatusCode()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(studentCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("api/students/" + student1.Id, contents);

            //Assert
            response.EnsureSuccessStatusCode();

        }

        [TestMethod]
        public async Task DeleteStudentById_ShouldHaveSuccessStatusCode()
        {
            //Act
            var response = await client.DeleteAsync("api/students/" + student1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
