using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Exam.Dto;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;

namespace Exam.Test.Integration
{
    [TestClass]
    public class ExamControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private Domain.Entities.Exam exam;
        private ExamDto examDto;
        private ExamCreatingDto examCreatingDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            exam = ExamTestUtils.GetExam();
            examDto = ExamTestUtils.GetExamDto(exam.Id);
            examCreatingDto = ExamTestUtils.GetExamCreatingDto();
        }

        [TestMethod]
        public async Task GetExamById_ShouldReturnExamDtoWithGivenId()
        {
            //Act
            var response = await client.GetAsync("api/exams/" + exam.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ExamDto examDtoReturned = JsonConvert.DeserializeObject<ExamDto>(responseString);
            examDtoReturned.Should().BeEquivalentTo(examDto);
        }

        [TestMethod]
        public async Task PostExam_ShouldReturnExamDtoFromGivenBody()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(examCreatingDto), Encoding.UTF8,
                "application/json");

            //Act
            var response = await client.PostAsync("api/exams", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ExamDto examDtoReturned = JsonConvert.DeserializeObject<ExamDto>(responseString);
            examDtoReturned.Should().BeEquivalentTo(examCreatingDto, options =>
                options.ExcludingMissingMembers());
        }

        [TestMethod]
        public async Task GetAllExamsFromCourseForStudent_ShouldReturnExamsForStudentWithThatId()
        {
            //Arrange
            List<ExamDto> examDtosExpected = new List<ExamDto> {ExamTestUtils.GetExamDto(exam.Id)};
            //Act
            var response = await client.GetAsync("api/students/" + StudentTestUtils.GetStudent().Id
                                                                 + "/courses/" + CourseTestUtils.GetCourse().Id
                                                                 + "/exams");
            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<ExamDto> examsDetailsDtosActual = JsonConvert.DeserializeObject<List<ExamDto>>(responseString);
            examsDetailsDtosActual.Should().BeEquivalentTo(examDtosExpected);
        }

        [TestMethod]
        public async Task getAllExamsForACourse_ShouldReturnExamsForThatCourse()
        {
            List<ExamDto> examDtosExpected = new List<ExamDto> { ExamTestUtils.GetExamDto(exam.Id) };
            var response = await client.GetAsync( "api/courses/" + CourseTestUtils.GetCourse().Id
                                                                 + "/exams");
            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<ExamDto> examsDetailsDtosActual = JsonConvert.DeserializeObject<List<ExamDto>>(responseString);
            examsDetailsDtosActual.Should().BeEquivalentTo(examDtosExpected);
        }
    }
}