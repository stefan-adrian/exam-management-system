using System.Collections.Generic;
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
        public IEnumerable<CourseDto> GetAll()
        {
            return courseService.GetAll();
        }
    }
}