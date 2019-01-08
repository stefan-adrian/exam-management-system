using AutoMapper;

namespace Exam.Business.ClassroomAllocation
{
    public class ClassroomAllocationMapper : IClassroomAllocationMapper
    {
        public Domain.Entities.ClassroomAllocation Map(Domain.Entities.Exam exam, Domain.Entities.Classroom classroom)
        {
            return new Domain.Entities.ClassroomAllocation(exam, classroom);
        }

        public ClassroomAllocationDetailsDto Map(Domain.Entities.ClassroomAllocation classroomAllocation)
        {
            return new ClassroomAllocationDetailsDto
            {
                ClassroomId = classroomAllocation.Classroom.Id,
                ExamId = classroomAllocation.Exam.Id
            };
        }
    }
}
