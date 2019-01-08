namespace Exam.Business.Classroom
{
    public class ClassroomMapper : IClassroomMapper
    {
        public ClassroomDetailsDto Map(Domain.Entities.Classroom classroom)
        {
            return new ClassroomDetailsDto
            {
                Id = classroom.Id,
                Capacity = classroom.Capacity,
                Location = classroom.Location
            };
        }

        public Domain.Entities.Classroom Map(ClassroomCreatingDto classroomCreatingDto)
        {
            return new Domain.Entities.Classroom(classroomCreatingDto.Location, classroomCreatingDto.Capacity);
        }
    }
}
