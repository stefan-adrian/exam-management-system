using Exam.Business.Grade.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Grade.Mapper
{
    public interface IGradeMapper
    {
        Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto, Domain.Entities.Student student,
            Domain.Entities.Exam exam);

        GradeDto Map(Guid gradeId, GradeEditingDto gradeEditingDto);

        Domain.Entities.Grade Map(GradeDto gradeDto,Domain.Entities.Grade grade);

        GradeDto Map(Domain.Entities.Grade grade);
    }
}
