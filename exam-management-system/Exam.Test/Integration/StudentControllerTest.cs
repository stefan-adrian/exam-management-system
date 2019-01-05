using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Course;
using Exam.Business.Student;
using Exam.Business.StudentCourse;
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
        private Course course1;
        private Course course2;
        private StudentDetailsDto studentDetailsDto1;
        private StudentDetailsDto studentDetailsDto2;
        private StudentCreationDto _studentCreationDto;
        private CourseDto courseDetailsDto;
        private StudentCourseCreationDto studentCourseCreationDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            student1 = StudentTestUtils.GetStudent();
            student2 = StudentTestUtils.GetStudent2();
            studentDetailsDto1 = StudentTestUtils.GetStudentDetailsDto(student1.Id);
            studentDetailsDto2 = StudentTestUtils.GetStudentDetailsDto(student2.Id);
            _studentCreationDto = StudentTestUtils.GetStudentCreationDto();
            course1 = CourseTestUtils.GetCourse();
            course2 = CourseTestUtils.GetCourse2();
            courseDetailsDto = CourseTestUtils.GetCourseDetailsDto(course1.Id);
            studentCourseCreationDto = StudentCourseTestUtils.GetStudentCourseCreationDto(course2.Id);
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
            var contents = new StringContent(JsonConvert.SerializeObject(_studentCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/students", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            StudentDetailsDto studentsDetailsDtoReturned = JsonConvert.DeserializeObject<StudentDetailsDto>(responseString);
            studentsDetailsDtoReturned.Should().BeEquivalentTo(_studentCreationDto, options =>
                 options.ExcludingMissingMembers());

        }

        [TestMethod]
        public async Task PutStudentById_ShouldHaveSuccessStatusCode()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(_studentCreationDto), Encoding.UTF8, "application/json");

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

        [TestMethod]
        public async Task GetCourses_ShouldReturnCoursesThatStudentJoinedTo()
        {
            // Arrange
            List<CourseDto> coursesDetailsDtos = new List<CourseDto>();
            coursesDetailsDtos.Add(courseDetailsDto);
            // Act
            var response = await client.GetAsync("api/students/" + student1.Id + "/courses");
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<CourseDto> studentsDetailsDtosReturned = JsonConvert.DeserializeObject<List<CourseDto>>(responseString);
            studentsDetailsDtosReturned.Should().BeEquivalentTo(coursesDetailsDtos);
        }

        [TestMethod]
        public async Task AddCourse_ShouldHaveSuccessStatusCode()
        {
            // Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(studentCourseCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("api/students/" + student2.Id + "/courses", contents);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
