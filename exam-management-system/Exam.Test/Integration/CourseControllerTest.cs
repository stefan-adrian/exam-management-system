using Exam.Api;
using Exam.Business.Course;
using Exam.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Exam.Test.TestUtils;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Integration
{
    [TestClass]
    public class CourseControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private Course course1;
        private Course course2;
        private CourseDto courseDetailsDto1;
        private CourseDto courseDetailsDto2;
        private CourseCreatingDto courseCreationDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            course1 = CourseTestUtils.GetCourse();
            course2 = CourseTestUtils.GetCourse2();
            courseDetailsDto1 = CourseTestUtils.GetCourseDetailsDto(course1.Id);
            courseDetailsDto2 = CourseTestUtils.GetCourseDetailsDto(course2.Id);
            courseCreationDto = CourseTestUtils.GetCourseCreatingDto();

        }

        [TestMethod]
        public async Task GetCourseById_ShouldReturnCourseWithGivenId()
        {
            //Act
            var response = await client.GetAsync("api/courses/" + course1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            CourseDto courseDetailsDtoReturned = JsonConvert.DeserializeObject<CourseDto>(responseString);
            courseDetailsDtoReturned.Should().BeEquivalentTo(courseDetailsDto1);
        }

        [TestMethod]
        public async Task GetAllCourses_ShouldReturnAllCourses()
        {
            //Arrange
            List<CourseDto> courseDetailsDtos = new List<CourseDto>();
            courseDetailsDtos.Add(courseDetailsDto1);
            courseDetailsDtos.Add(courseDetailsDto2);

            //Act
            var response = await client.GetAsync("api/courses");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<CourseDto> coursesDetailsDtosReturned = JsonConvert.DeserializeObject<List<CourseDto>>(responseString);
            coursesDetailsDtosReturned.Should().BeEquivalentTo(courseDetailsDtos);
        }

        [TestMethod]
        public async Task GetAllProfessorCourses_ShouldReturnAllCourses()
        {
            //Arrange
            List<CourseDto> courseDetailsDtos = new List<CourseDto>();
            courseDetailsDtos.Add(courseDetailsDto1);
            courseDetailsDtos.Add(courseDetailsDto2);
            //Act
            var response = await client.GetAsync("api/professors/" + ProfessorTestUtils.GetProfessor().Id + "/courses");
            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<CourseDto> coursesDetailsDtosReturned = JsonConvert.DeserializeObject<List<CourseDto>>(responseString);
            coursesDetailsDtosReturned.Should().BeEquivalentTo(courseDetailsDtos);
        }

        [TestMethod]
        public async Task PostCourse_ShouldReturnCourseCreatedFromGivenBody()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(courseCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/professors/" + ProfessorTestUtils.GetProfessor().Id + "/courses", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            CourseDto courseDetailsDtoReturned = JsonConvert.DeserializeObject<CourseDto>(responseString);
            courseDetailsDtoReturned.Should().BeEquivalentTo(courseCreationDto, options =>
                 options.ExcludingMissingMembers());

        }

        [TestMethod]
        public async Task PutCourseById_ShouldHaveSuccessStatusCode()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(courseCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("api/courses/" + course1.Id, contents);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task DeleteStudentById_ShouldHaveSuccessStatusCode()
        {
            //Act
            var response = await client.DeleteAsync("api/courses/" + course1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
