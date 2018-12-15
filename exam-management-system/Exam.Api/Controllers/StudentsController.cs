using System;
using System.Threading.Tasks;
using Exam.Business.Student;
using Exam.Business.Student.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentService.GetAll();
            return Ok(students);
        }

        [HttpGet("{id:guid}", Name = "FindStudentById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var student = await studentService.GetById(id);
                return Ok(student);
            }
            catch (StudentNotFoundException studentNotFoundException)
            {
                return NotFound(studentNotFoundException.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreationDto studentCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var student = await studentService.Create(studentCreationDto);

            return CreatedAtRoute("FindStudentById", new { id = student.Id }, student);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudentCreationDto studentCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = await studentService.Update(id, studentCreationDto);
                return NoContent();
            }
            catch (StudentNotFoundException studentNotFoundException)
            {
                return NotFound(studentNotFoundException.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await studentService.Delete(id);
                return Ok();
            }
            catch (StudentNotFoundException studentNotFoundException)
            {
                return NotFound(studentNotFoundException.Message);
            }
        }
    }
}