using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Exam.Business.Course;
using Exam.Business.Grade.Dto;

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
            return new Domain.Entities.Grade(student, exam);
        }

        public GradeDto Map(Guid gradeId, GradeEditingDto gradeEditingDto)
        {
            GradeDto gradeDto = new GradeDto(gradeId, gradeEditingDto.Value,gradeEditingDto.Pages,
                gradeEditingDto.Date,gradeEditingDto.Agree,gradeEditingDto.StudentId,gradeEditingDto.ExamId)
            {
                Id = gradeId
            };

            return gradeDto;
        }

        public Domain.Entities.Grade Map(GradeDto gradeDto,Domain.Entities.Grade grade)
        {
            return this.autoMapper.Map(gradeDto, grade);
        }

        public GradeDto Map(Domain.Entities.Grade grade)
        {
            return new GradeDto(grade.Id, grade.Value, grade.Pages, grade.Date, grade.Agree, grade.Student.Id, grade.Exam.Id);
        }
    }
}
