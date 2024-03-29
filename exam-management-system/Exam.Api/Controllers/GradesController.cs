﻿using System;
using System.Threading.Tasks;
using Exam.Business.Exam.Exception;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Exception;
using Exam.Business.Grade.Service;
using Exam.Business.Student.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService gradeService;

        public GradesController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost("grades")]
        public async Task<IActionResult> CreateGrade([FromBody] GradeCreationDto gradeCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var grade = await gradeService.Create(gradeCreationDto);
                return Ok(grade);
            }
            catch (ExamNotFoundException examNotFoundException)
            {
                return NotFound(examNotFoundException.Message);
            }
            catch (StudentNotFoundException studentNotFoundException)
            {
                return NotFound(studentNotFoundException.Message);
            }
        }

        [HttpPut("grades/{gradeId:guid}")]
        public async Task<IActionResult> UpdateGrade(Guid gradeId, GradeEditingDto gradeEditingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingGrade = await this.gradeService.Update(gradeId, gradeEditingDto);
                return NoContent();
            }
            catch (GradeNotFoundException gradeNotFoundException)
            {
                return NotFound(gradeNotFoundException.Message);
            }
        }

        [HttpGet("students/{studentId:guid}/exams/{examId:guid}/grade")]
        public async Task<IActionResult> GetStudentExamGrade(Guid studentId, Guid examId)
        {
            try
            {
                var grade = await gradeService.GetStudentExamGrade(studentId, examId);
                return Ok(grade);
            }
            catch (GradeNotFoundException gradeNotFoundException)
            {
                return NotFound(gradeNotFoundException.Message);
            }
        }

        [HttpGet("exams/{examId:guid}/grades")]
        public async Task<IActionResult> GetAllGradesByExam(Guid examId)
        {
            try
            {
                var grades = await gradeService.GetAllGradesByExam(examId);
                return Ok(grades);
            }
            catch (ExamNotFoundException examNotFoundException)
            {
                return NotFound(examNotFoundException.Message);
            }
        }
    }
}