﻿using System;
using System.Collections.Generic;

namespace Exam.Business.Course
{
    public interface ICourseService
    {
        Domain.Entities.Course Create();
        IEnumerable<CourseDto> GetAll();
        CourseDto GetById(Guid id);
    }
}
