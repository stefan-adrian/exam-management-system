using System;
using Exam.Business.ClassroomAllocation;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class ClassroomAllocationTestUtils
    {
        private static ClassroomAllocation classroomAllocation = null;

        public static ClassroomAllocation GetClassroomAllocation()
        {
            if (classroomAllocation == null)
            {
                classroomAllocation = new ClassroomAllocation(ExamTestUtils.GetExam(), ClassroomTestUtils.GetClassroom());
            }

            return classroomAllocation;
        }

        public static ClassroomAllocationCreatingDto GetClassroomAllocationCreatingDto()
        {
            return new ClassroomAllocationCreatingDto
            {
                ClassroomId = ClassroomTestUtils.GetClassroom().Id,
                ExamId = ExamTestUtils.GetExam().Id
            };
        }

        public static ClassroomAllocationDetailsDto GetClassroomAllocationDetailsDto(Guid id)
        {
            return new ClassroomAllocationDetailsDto
            {
                Id = id,
                ClassroomId = ClassroomTestUtils.GetClassroom().Id,
                ExamId = ExamTestUtils.GetExam().Id
            };
        }
    }
}
