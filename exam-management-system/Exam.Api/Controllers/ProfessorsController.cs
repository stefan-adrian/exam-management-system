using System;
using System.Threading.Tasks;
using Exam.Business.Professor;
using Exam.Business.Professor.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/professors")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorService professorService;

        public ProfessorsController(IProfessorService professorService)
        {
            this.professorService = professorService ?? throw new ArgumentNullException();
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
            try
            {
                var professor = await this.professorService.GetProfessorDetailsDtoById(professorId);
                return Ok(professor);
            }
            catch (ProfessorNotFoundException professorNotFoundException)
            {
                return NotFound(professorNotFoundException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfessorCreatingDto professorCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var professor = await this.professorService.Create(professorCreatingDto);
            return CreatedAtRoute("FindProfessorById", new { professorId = professor.Id }, professor);
        }

        [HttpPut("{professorId:guid}")]
        public async Task<IActionResult> Update(Guid professorId, [FromBody] ProfessorCreatingDto professorCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingProfessor = await this.professorService.Update(professorId, professorCreatingDto);
                return NoContent();
            }
            catch (ProfessorNotFoundException professorNotFoundException)
            {
                return NotFound(professorNotFoundException.Message);
            }
        }

        [HttpDelete("{professorId:guid}")]
        public async Task<IActionResult> Delete(Guid professorId)
        {
            try
            {
                await this.professorService.Delete(professorId);
                return Ok();
            }
            catch (ProfessorNotFoundException professorNotFoundException)
            {
                return NotFound(professorNotFoundException.Message);
            }
        }
    }
}