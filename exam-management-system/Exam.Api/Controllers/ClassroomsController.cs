using System;
using System.Threading.Tasks;
using Exam.Business.Classroom;
using Exam.Business.Classroom.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/classrooms")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService classroomService;

        public ClassroomsController(IClassroomService classroomService)
        {
            this.classroomService = classroomService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classrooms = await this.classroomService.GetAll();
            return Ok(classrooms);
        }

        [HttpGet("{classroomId:Guid}", Name = "FindClassroomById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var classroom = await this.classroomService.GetDetailsDtoById(id);
                return Ok(classroom);
            }
            catch (ClassroomNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassroomCreatingDto classroomCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var classroom = await this.classroomService.Create(classroomCreatingDto);
                return CreatedAtRoute("FindClassroomById", new { classroomId = classroom.Id }, classroom);
            }
            catch (ClassroomLocationAlreadyExistsException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}