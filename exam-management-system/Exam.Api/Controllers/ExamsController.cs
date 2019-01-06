using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ExamsController(IExamService examService)
        {
            this.examService = examService ?? throw new ArgumentNullException();
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
    }
}