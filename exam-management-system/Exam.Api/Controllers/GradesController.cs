using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Grade.Dto;
using Exam.Business.Grade.Service;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService gradeService;

        public GradesController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] GradeCreationDto gradeCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await gradeService.Create(gradeCreationDto);

            return Ok(grade);
        }

    }
}
