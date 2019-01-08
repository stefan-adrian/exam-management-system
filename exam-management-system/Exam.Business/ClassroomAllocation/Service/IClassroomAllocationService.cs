using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Business.ClassroomAllocation
{
    public interface IClassroomAllocationService
    {
        Task<List<ClassroomAllocationDetailsDto>> GetByExam(Guid examId);

        Task<ClassroomAllocationDetailsDto> Create(ClassroomAllocationCreatingDto classroomAllocationCreatingDto);

        Task CheckIn(Guid classroomAllocationId, Guid studentId);
    }
}
