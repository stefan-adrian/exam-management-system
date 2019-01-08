﻿using Exam.Business.Grade.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Grade.Mapper
{
    public interface IGradeMapper
    {
        Domain.Entities.Grade Map(GradeCreationDto gradeCreationDto, Domain.Entities.Student student,
            Domain.Entities.Exam exam);

        GradeDto Map(Domain.Entities.Grade grade);
    }
}
