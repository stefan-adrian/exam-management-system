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
            var courses = courseService.GetAll();
            return Ok(courses);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindCourseById(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
    }
}