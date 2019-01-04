using System;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Exam.Business.Professor.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService ?? throw new ArgumentNullException();
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await courseService.GetAll();
            return Ok(courses);
        }

        [HttpGet("professors/{professorId:guid}/courses")]
        public async Task<IActionResult> GetAllProfessorCourses(Guid professorId)
        {
            try
            {
                var courses = await courseService.GetAllForProfessor(professorId);
                return Ok(courses);
            }
            catch (ProfessorNotFoundException professorNotFoundException)
            {
                return NotFound(professorNotFoundException.Message);
            }
        }

        [HttpGet("courses/{courseId:guid}", Name = "FindCourseById")]
        public async Task<IActionResult> FindCourseById(Guid courseId)
        {
            try
            {
                var course = await this.courseService.GetById(courseId);
                return Ok(course);
            }
            catch (CourseNotFoundException courseNotFoundException)
            {
                return NotFound(courseNotFoundException.Message);
            }
        }

        [HttpPost("professors/{professorId:guid}/courses")]
        public async Task<IActionResult> Create(Guid professorId, [FromBody] CourseCreatingDto courseCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await this.courseService.Create(professorId, courseCreatingDto);
                return CreatedAtRoute("FindCourseById", new {courseId = course.Id}, course);
            }
            catch (ProfessorNotFoundException professorNotFoundException)
            {
                return NotFound(professorNotFoundException.Message);
            }
        }

        [HttpPut("courses/{courseId:guid}")]
        public async Task<IActionResult> Update(Guid courseId, [FromBody] CourseCreatingDto courseCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingCourse = await this.courseService.Update(courseId, courseCreatingDto);
                return NoContent();
            }
            catch (CourseNotFoundException courseNotFoundException)
            {
                return NotFound(courseNotFoundException.Message);
            }
        }

        [HttpDelete("courses/{courseId:guid}")]
        public async Task<IActionResult> Delete(Guid courseId)
        {
            try
            {
                await this.courseService.Delete(courseId);
                return Ok();
            }
            catch (CourseNotFoundException courseNotFoundException)
            {
                return NotFound(courseNotFoundException.Message);
            }
        }
    }
}