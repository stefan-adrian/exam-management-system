using System;
using System.Threading.Tasks;
using Exam.Business.Course;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await courseService.GetAll();
            return Ok(courses);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindCourseById(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await this.courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Year")] CourseCreatingDto courseCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var course = await this.courseService.Create(courseCreatingDto);
            return CreatedAtRoute("FindCourseById", new { courseId = course.Id }, course);
        }

        [HttpPut("{courseId:guid}")]
        public async Task<IActionResult> Update(Guid courseId, [Bind("Name, Year")] CourseCreatingDto courseCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = await this.courseService.Update(courseId, courseCreatingDto);
            return CreatedAtRoute("FindCourseById", new { courseId = existingCourse.Id }, existingCourse);
        }

        [HttpDelete("{courseId:guid}")]
        public async Task<IActionResult> Delete(Guid courseId)
        {
            await this.courseService.Delete(courseId);
            return Ok();
        }
    }
}