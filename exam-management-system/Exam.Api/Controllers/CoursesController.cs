using System;
using System.Threading.Tasks;
using Exam.Business.Course;
using Exam.Business.Course.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await courseService.GetAll();
            return Ok(courses);
        }


        [HttpGet("{courseId:guid}", Name = "FindCourseById")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreatingDto courseCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var course = await this.courseService.Create(courseCreatingDto);
            return CreatedAtRoute("FindCourseById", new { courseId = course.Id }, course);
        }

        [HttpPut("{courseId:guid}")]
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

        [HttpDelete("{courseId:guid}")]
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