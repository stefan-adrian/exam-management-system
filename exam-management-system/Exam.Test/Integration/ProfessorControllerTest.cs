using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exam.Api;
using Exam.Business.Professor;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;

namespace Exam.Test.Integration
{
    [TestClass]
    public class ProfessorControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private Professor professor1;
        private Professor professor2;
        private ProfessorDetailsDto professorDetailsDto1;
        private ProfessorDetailsDto professorDetailsDto2;
        private ProfessorCreatingDto professorCreationDto;

        [TestInitialize]
        public void Setup()
        {
            client = new CustomWebApplicationFactory<Startup>().CreateClient();
            professor1 = ProfessorTestUtils.GetProfessor();
            professor2 = ProfessorTestUtils.GetProfessor2();
            professorDetailsDto1 = ProfessorTestUtils.GetProfessorDetailsDto(professor1.Id);
            professorDetailsDto2 = ProfessorTestUtils.GetProfessorDetailsDto(professor2.Id);
            professorCreationDto = ProfessorTestUtils.GetProfessorCreatingDto();
        }

        [TestMethod]
        public async Task GetProfessorById_ShouldReturnProfessorDetailsDtoWithGivenId()
        {
            //Act
            var response = await client.GetAsync("api/professors/" + professor1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ProfessorDetailsDto professorDetailsDtoReturned = JsonConvert.DeserializeObject<ProfessorDetailsDto>(responseString);
            professorDetailsDtoReturned.Should().BeEquivalentTo(professorDetailsDto1);
        }

        [TestMethod]
        public async Task GetAllProfessors_ShouldReturnAllProfessorsDetailsDtos()
        {
            //Arrange
            List<ProfessorDetailsDto> professorDetailsDtos = new List<ProfessorDetailsDto>();
            professorDetailsDtos.Add(professorDetailsDto1);
            professorDetailsDtos.Add(professorDetailsDto2);

            //Act
            var response = await client.GetAsync("api/professors");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<ProfessorDetailsDto> professorDetailsDtosReturned = JsonConvert.DeserializeObject<List<ProfessorDetailsDto>>(responseString);
            professorDetailsDtosReturned.Should().BeEquivalentTo(professorDetailsDtos);
        }

        [TestMethod]
        public async Task PostProfessor_ShouldReturnProfessorCreatedFromGivenBody()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(professorCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("api/professors", contents);

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ProfessorDetailsDto professorDetailsDtoReturned = JsonConvert.DeserializeObject<ProfessorDetailsDto>(responseString);
            professorDetailsDtoReturned.Should().BeEquivalentTo(professorCreationDto, options =>
                 options.ExcludingMissingMembers());

        }

        [TestMethod]
        public async Task PutProfessorById_ShouldHaveSuccessStatusCode()
        {
            //Arrange
            var contents = new StringContent(JsonConvert.SerializeObject(professorCreationDto), Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("api/professors/" + professor1.Id, contents);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task DeleteProfessorById_ShouldHaveSuccessStatusCode()
        {
            //Act
            var response = await client.DeleteAsync("api/professors/" + professor1.Id);

            //Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
