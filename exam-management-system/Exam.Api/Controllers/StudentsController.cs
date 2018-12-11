using System;
using System.Threading.Tasks;
using Exam.Business.Student;
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
            this.studentService = studentService;
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
            var student = await studentService.GetById(id);
            return Ok(student);
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
            var student = await studentService.Update(id, studentCreationDto);
            return CreatedAtRoute("FindStudentById", new { studentId = student.Id }, student);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await studentService.Delete(id);
            return Ok();
        }
    }
}