using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Professor
{
    public interface IProfessorService
    {
        Task<List<ProfessorDetailsDto>> GetAll();

        Task<ProfessorDetailsDto> GetById(Guid id);

        Task<ProfessorDetailsDto> Create(ProfessorCreatingDto newProfessor);
    }
}
