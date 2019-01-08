using System;
using System.Threading.Tasks;
using Exam.Business.ClassroomAllocation;
using Exam.Business.Course.Exception;
using Exam.Business.Exam.Exception;
using Microsoft.AspNetCore.Mvc;
using Exam.Business.Exam.Service;
using Exam.Business.Exam.Dto;
using Exam.Business.Student.Exception;

namespace Exam.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService examService;
        private readonly IClassroomAllocationService classroomAllocationService;

        public ExamsController(IExamService examService, IClassroomAllocationService classroomAllocationService)
        {
            this.examService = examService ?? throw new ArgumentNullException();
            this.classroomAllocationService = classroomAllocationService ?? throw new ArgumentNullException();
        }

        [HttpGet("exams/{id:guid}", Name = "FindExamById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var exam = await this.examService.GetDtoById(id);
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

            try
            {
                var exam = await examService.Create(examCreatingDto);

                return CreatedAtRoute("FindExamById", new {id = exam.Id}, exam);
            }
            catch (CourseNotFoundException courseNotFoundException)
            {
                return NotFound(courseNotFoundException.Message);
            }
        }

        [HttpGet("exams/{id:Guid}/classroomAllocation")]
        public async Task<IActionResult> GetClassroomAllocation(Guid id)
        {
            try
            {
                var result = await this.classroomAllocationService.GetByExam(id);
                return Ok(result);
            }
            catch (ExamNotFoundException e)
            {
                return NotFound(e.Message);
            }
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

        [HttpGet("exams/{examId:guid}/checked-in-students")]
        public async Task<IActionResult> GetCheckedInStudentsFotExam(Guid examId)
        {
            try
            {
                var students = await examService.GetCheckedInStudents(examId);
                return Ok(students);
            }
            catch (ExamNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}