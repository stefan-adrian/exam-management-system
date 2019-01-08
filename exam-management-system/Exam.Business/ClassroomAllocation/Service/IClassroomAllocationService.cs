using System;
using System.Threading.Tasks;

namespace Exam.Business.ClassroomAllocation
{
    public interface IClassroomAllocationService
    {
        Task<ClassroomAllocationDetailsDto> GetByExam(Guid examId);

        Task<ClassroomAllocationDetailsDto> Create(ClassroomAllocationCreatingDto classroomAllocationCreatingDto);

        Task CheckIn(Guid classroomAllocationId, Guid studentId);
    }
}
