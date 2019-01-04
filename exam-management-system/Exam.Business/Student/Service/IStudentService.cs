using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam.Business.Student
{
    public interface IStudentService
    {
        Task<List<StudentDetailsDto>> GetAll();

        Task<Domain.Entities.Student> GetStudentById(Guid id);

        Task<StudentDetailsDto> GetById(Guid id);

        Task<StudentDetailsDto> Create(StudentCreationDto studentCreationDto);

        Task<StudentDetailsDto> Update(Guid id, StudentCreationDto studentCreationDto);

        Task Delete(Guid id);
    }
}
