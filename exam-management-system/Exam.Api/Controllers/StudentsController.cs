using System;
using System.Threading.Tasks;
using Exam.Business.Course.Exception;
using Exam.Business.Student;
using Exam.Business.Student.Exception;
using Exam.Business.StudentCourse;
using Exam.Business.StudentCourse.Exception;
using Exam.Business.StudentCourse.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly IStudentCourseService studentCourseService;

        public StudentsController(IStudentService studentService, IStudentCourseService studentCourseService)
        {
            this.studentService = studentService ?? throw new ArgumentNullException();
            this.studentCourseService = studentCourseService ?? throw new ArgumentNullException();
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

        [HttpGet("{id:guid}/courses")]
        public async Task<IActionResult> GetCourses(Guid id)
        {
            try
            {
                var courses = await this.studentCourseService.GetCourses(id);
                return Ok(courses);
            }
            catch (StudentNotFoundException studentNotFoundException)
            {
                return NotFound(studentNotFoundException.Message);
            }
        }

        [HttpPut("{id:guid}/courses")]
        public async Task<IActionResult> AddCourse(Guid id,
            [FromBody] StudentCourseCreationDto studentCourseCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.studentCourseService.AddCourse(id, studentCourseCreationDto);
                return NoContent();
            }
            catch (Exception exception) when (exception is StudentNotFoundException ||
                                              exception is CourseNotFoundException)
            {
                return NotFound(exception.Message);
            }
            catch (StudentCannotApplyException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}