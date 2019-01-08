﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Grade.Exception
{
    public class GradeNotFoundException : SystemException
    {
        public GradeNotFoundException(Guid studentId, Guid examId)
            : base("Grade with student id " + studentId + " and exam id " + examId + " not found!")
        {
        }
    }
}
