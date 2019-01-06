using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Exam.Dto;
using Exam.Domain.Entities;
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
            var contents = new StringContent(JsonConvert.SerializeObject(examCreatingDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/exams", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ExamDto examDtoReturned = JsonConvert.DeserializeObject<ExamDto>(responseString);
            examDtoReturned.Should().BeEquivalentTo(examCreatingDto, options =>
                options.ExcludingMissingMembers());
//            examDtoReturned.Should().Match<ExamDto>((obj) =>
//                obj.Date == this.examDto.Date);

        }
    }
}
