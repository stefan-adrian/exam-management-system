using System;
using Exam.Business.Classroom;
using Exam.Domain.Entities;

namespace Exam.Test.TestUtils
{
    public class ClassroomTestUtils
    {
        private static Classroom classroom1 = null;
        private static Classroom classroom2 = null;

        public static Classroom GetClassroom()
        {
            if (classroom1 == null)
            {
                classroom1 = new Classroom("C112", 80);
            }

            return classroom1;
        }

        public static Classroom GetClassroom2()
        {
            if (classroom2 == null)
            {
                classroom2 = new Classroom("C2", 150);
            }

            return classroom2;
        }

        public static ClassroomDetailsDto GetClassroomDetailsDto(Guid id)
        {
            return new ClassroomDetailsDto
            {
                Id = id,
                Capacity = 80,
                Location = "C112"
            };
        }

        public static ClassroomCreatingDto GetClassroomCreatingDto()
        {
            return new ClassroomCreatingDto
            {
                Capacity = 80,
                Location = "C112"
            };
        }
    }
}
