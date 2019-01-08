using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.ClassroomAllocation;
using Microsoft.AspNetCore.Http;
using Exam.Business.Exam;
using Exam.Business.Exam.Exception;
using Microsoft.AspNetCore.Mvc;
using Exam.Business.Exam.Service;
using Exam.Business.Exam.Dto;

namespace Exam.Api.Controllers
{
    [Route("api/exams")]
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

        [HttpGet("{id:guid}", Name = "FindExamById")]
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

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] ExamCreatingDto examCreatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = await examService.Create(examCreatingDto);

            return CreatedAtRoute("FindExamById", new { id = exam.Id }, exam);
        }

        [HttpGet("{id:Guid}/classroomAllocation")]
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
    }
}