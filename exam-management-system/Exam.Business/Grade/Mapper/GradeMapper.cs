using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Exam.Business.Grade.Dto;
using Exam.Business.Student;

namespace Exam.Business.Grade.Mapper
{
    public class GradeMapper : IGradeMapper
    {
        private readonly IMapper autoMapper;

        public GradeMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<GradeDto, Domain.Entities.Grade>(); }).CreateMapper();
        }

        public Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto,
            Domain.Entities.Student student, Domain.Entities.Exam exam)
        {
            return new Domain.Entities.Grade(gradeCreationDto.Pages, student, exam);
        }

        public GradeDto Map(Domain.Entities.Grade grade)
        {
            return new GradeDto(grade.Id, grade.Value, grade.Pages, grade.Date, grade.Student.Id, grade.Exam.Id);
        }

        public Domain.Entities.Grade Map(GradeDto gradeDto, Domain.Entities.Grade grade)
        {
            autoMapper.Map(gradeDto, grade);
            return grade;
        }
    }
}
