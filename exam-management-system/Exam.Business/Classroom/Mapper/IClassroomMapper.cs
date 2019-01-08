namespace Exam.Business.Classroom
{
    public interface IClassroomMapper
    {
        ClassroomDetailsDto Map(Domain.Entities.Classroom classroom);

        Domain.Entities.Classroom Map(ClassroomCreatingDto classroomCreatingDto);
    }
}
