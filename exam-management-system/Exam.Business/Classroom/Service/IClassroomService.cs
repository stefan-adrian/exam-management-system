using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Business.Classroom
{
    public interface IClassroomService
    {
        Task<List<ClassroomDetailsDto>> GetAll();

        Task<ClassroomDetailsDto> GetDetailsDtoById(Guid id);

        Task<ClassroomDetailsDto> Create(ClassroomCreatingDto classroomCreatingDto);
    }
}
