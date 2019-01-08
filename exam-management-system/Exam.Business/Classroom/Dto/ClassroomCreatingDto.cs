using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Business.Classroom
{
    public class ClassroomCreatingDto
    {
        [Required]
        public String Location { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
