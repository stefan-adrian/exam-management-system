using System;
using System.Threading.Tasks;
using Exam.Business.Course;
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
            this.courseService = courseService;
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
            if (courseId == null)
            {
                return NotFound();
            }
            var course = await this.courseService.GetById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
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

            var existingCourse = await this.courseService.Update(courseId, courseCreatingDto);
            return NoContent();
        }

        [HttpDelete("{courseId:guid}")]
        public async Task<IActionResult> Delete(Guid courseId)
        {
            await this.courseService.Delete(courseId);
            return Ok();
        }
    }
}