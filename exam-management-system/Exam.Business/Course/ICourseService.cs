﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Business.Course
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAll();
        Task<CourseDto> GetById(Guid id);
        Task<CourseDto> Create(CourseCreatingDto newCourse);
        Task<CourseDto> Update(Guid existingCourseId, CourseCreatingDto courseCreatingDto);
        Task Delete(Guid existingCourseId);
    }
}