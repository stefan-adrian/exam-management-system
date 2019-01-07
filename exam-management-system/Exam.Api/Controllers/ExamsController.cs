using System;
using System.Threading.Tasks;
using Exam.Business.Course.Exception;
using Exam.Business.Exam.Exception;
using Microsoft.AspNetCore.Mvc;
using Exam.Business.Exam.Service;
using Exam.Business.Exam.Dto;
using Exam.Business.Professor.Exception;
using Exam.Business.Student.Exception;

namespace Exam.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService examService;

        public ExamsController(IExamService examService)
        {
            this.examService = examService ?? throw new ArgumentNullException();
        }

        [HttpGet("exams/{id:guid}", Name = "FindExamById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var exam = await this.examService.GetById(id);
                return Ok(exam);
            }
            catch (ExamNotFoundException examNotFoundException)
            {
                return NotFound(examNotFoundException.Message);
            }
        }

        [HttpPost("exams")]
        public async Task<IActionResult> CreateExam([FromBody] ExamCreatingDto examCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = await examService.Create(examCreatingDto);

            return CreatedAtRoute("FindExamById", new {id = exam.Id}, exam);
        }

        [HttpGet("students/{studentId:guid}/courses/{courseId:guid}/exams")]
        public async Task<IActionResult> GetAllExamsFromCourseForStudent(Guid courseId, Guid studentId)
        {
            try
            {
                var exams = await examService.GetAllExamsFromCourseForStudent(courseId, studentId);
                return Ok(exams);
            }
            catch (Exception exception) when (exception is CourseNotFoundException ||
                                              exception is StudentNotFoundException)
            {
                return NotFound(exception.Message);
            }
        }
    }
}