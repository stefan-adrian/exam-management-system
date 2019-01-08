using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Grade.Dto;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;

namespace Exam.Test.Integration
{
    [TestClass]
    public class GradeControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private Grade grade;
        private GradeDto gradeDto;
        private GradeCreationDto gradeCreationDto;
        private GradeEditingDto gradeEditingDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            this.grade = GradeTestUtils.GetInitialStateGrade();
            this.gradeDto = GradeTestUtils.GetInitialGradeDto(grade.Id);
            this.gradeCreationDto = GradeTestUtils.GetGradeCreationDto();
            this.gradeEditingDto = GradeTestUtils.GetGradeEditingDto();
        }

        [TestMethod]
        public async Task GetStudentExamGrade_ShouldReturnGradeDtoWithGivenId()
        {
            //Act
            var response = await client.GetAsync("api/students/" + grade.Student.Id+"/exams/"+grade.Exam.Id+"/grade");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            GradeDto gradeDtoReturned = JsonConvert.DeserializeObject<GradeDto>(responseString);
            gradeDtoReturned.Should().BeEquivalentTo(gradeDto, options =>
                options.Excluding(g => g.Date).Excluding(g => g.Value));
        }

        [TestMethod]
        public async Task PostGrade_ShouldReturnGradeDtoFromGivenBody()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(gradeCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/grades", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            GradeDto gradeDtoReturned = JsonConvert.DeserializeObject<GradeDto>(responseString);
            gradeDtoReturned.Should().BeEquivalentTo(gradeDtoReturned, options =>
                options.Excluding(g => g.Id).Excluding(g => g.Date).Excluding(g => g.Value));
        }

        [TestMethod]
        public async Task PutGradeById_ShouldHaveSuccessStatusCode()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(gradeEditingDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("api/grades/" + grade.Id, contents);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
