using Exam.Business.Student;
using Exam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public ActionResult<Student> Get()
        {
            return studentService.Create();
        }
    }
}