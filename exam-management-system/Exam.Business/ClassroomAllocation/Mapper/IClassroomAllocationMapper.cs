using System;
using System.Collections.Generic;
using Exam.Business.Exam.Dto;

namespace Exam.Business.ClassroomAllocation
{
    public interface IClassroomAllocationMapper
    {
        Domain.Entities.ClassroomAllocation Map(Domain.Entities.Exam exam, Domain.Entities.Classroom classroom);

        ClassroomAllocationDetailsDto Map(Domain.Entities.ClassroomAllocation classroomAllocation);

        List<ClassroomAllocationCreatingDto> Map(ExamCreatingDto examCreatingDto, Guid examId);
    }
}
