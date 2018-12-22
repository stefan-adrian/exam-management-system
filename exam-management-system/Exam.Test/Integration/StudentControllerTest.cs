using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Student;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Exam.Test.Integration
{

    public class StudentControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly Student _student1;
        private readonly Student _student2;
        private readonly StudentDetailsDto _studentDetailsDto1;
        private readonly StudentDetailsDto _studentDetailsDto2;
        private readonly StudentCreationDto _studentCreationDto;

        public StudentControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _student1 = StudentTestUtils.GetStudent();
            _student2 = StudentTestUtils.GetStudent2();
            _studentDetailsDto1 = StudentTestUtils.GetStudentDetailsDto(_student1.Id);
            _studentDetailsDto2 = StudentTestUtils.GetStudentDetailsDto(_student2.Id);
            _studentCreationDto = StudentTestUtils.GetStudentCreationDto();
        }

        [Fact]
        public async Task GetStudentById()
        {

            //Act
            var response = await _client.GetAsync("api/students/" + _student1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            StudentDetailsDto studentsDetailsDtoReturned = JsonConvert.DeserializeObject<StudentDetailsDto>(responseString);
            studentsDetailsDtoReturned.Should().BeEquivalentTo(_studentDetailsDto1);
        }

        [Fact]
        public async Task GetAllStudents()
        {
            //Arrange
            List<StudentDetailsDto> studentDetailsDtos = new List<StudentDetailsDto>();
            studentDetailsDtos.Add(_studentDetailsDto1);
            studentDetailsDtos.Add(_studentDetailsDto2);

            //Act
            var response = await _client.GetAsync("api/students");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<StudentDetailsDto> studentsDetailsDtosReturned = JsonConvert.DeserializeObject<List<StudentDetailsDto>>(responseString);
            studentsDetailsDtosReturned.Should().BeEquivalentTo(studentDetailsDtos);
        }

        [Fact]
        public async Task PostStudentById()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(_studentCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("api/students", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            StudentDetailsDto studentsDetailsDtoReturned = JsonConvert.DeserializeObject<StudentDetailsDto>(responseString);
            studentsDetailsDtoReturned.Should().BeEquivalentTo(_studentCreationDto, options =>
                 options.ExcludingMissingMembers());

        }

        [Fact]
        public async Task PutStudentById()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(_studentCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PutAsync("api/students/" + _student1.Id, contents);

            //Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task DeleteStudentById()
        {
            //Act
            var response = await _client.DeleteAsync("api/students/" + _student1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
