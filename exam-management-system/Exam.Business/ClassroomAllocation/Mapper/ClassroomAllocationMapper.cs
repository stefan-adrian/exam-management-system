using System;
using System.Collections.Generic;
using Exam.Business.Exam.Dto;

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
                Id = classroomAllocation.Id,
                ClassroomId = classroomAllocation.Classroom.Id,
                ExamId = classroomAllocation.Exam.Id
            };
        }

        public List<ClassroomAllocationCreatingDto> Map(ExamCreatingDto examCreatingDto, Guid examId)
        {
            var classroomAllocations = new List<ClassroomAllocationCreatingDto>();
            examCreatingDto.Classrooms.ForEach(classroom => classroomAllocations.Add(new ClassroomAllocationCreatingDto
            {
                ClassroomId = classroom,
                ExamId = examId
            }));
            return classroomAllocations;
        }
    }
}
