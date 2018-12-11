using System;
using System.Threading.Tasks;
using Exam.Business.Professor;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorService professorService;

        public ProfessorsController(IProfessorService professorService)
        {
            this.professorService = professorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await this.professorService.GetAll();
            return Ok(professors);
        }

        [HttpGet("{professorId:guid}", Name = "FindProfessorById")]
        public async Task<IActionResult> GetById(Guid professorId)
        {
            var professor = await this.professorService.GetById(professorId);
            return Ok(professor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("RegistrationNumber, Email, Password, FirstName, LastName")] ProfessorCreatingDto professorCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var professor = await this.professorService.Create(professorCreatingDto);
            return CreatedAtRoute("FindProfessorById", new { professorId = professor.Id }, professor);
        }
    }
}