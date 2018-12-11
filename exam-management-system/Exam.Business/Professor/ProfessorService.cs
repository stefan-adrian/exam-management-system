using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Professor
{
    public class ProfessorService : IProfessorService
    {
        private readonly IReadRepository readRepository;

        private readonly IWriteRepository writeRepository;

        private readonly ProfessorMapper professorMapper;

        public ProfessorService(IReadRepository readRepository, IWriteRepository writeRepository,
            ProfessorMapper professorMapper)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
            this.professorMapper = professorMapper;
        }

        public Task<List<ProfessorDetailsDto>> GetAll()
        {
            return GetAllProfessorDetailsDtos().ToListAsync();
        }

        public Task<ProfessorDetailsDto> GetById(Guid id)
        {
            return GetAllProfessorDetailsDtos().SingleOrDefaultAsync(professor => professor.Id == id);
        }

        public async Task<ProfessorDetailsDto> Create(ProfessorCreatingDto newProfessor)
        {
            Domain.Entities.Professor professor = professorMapper.map(newProfessor);
            await this.writeRepository.AddNewAsync(professor);
            await this.writeRepository.SaveAsync();
            return professorMapper.map(professor);
        }

        private IQueryable<ProfessorDetailsDto> GetAllProfessorDetailsDtos()
        {
            return this.readRepository.GetAll<Domain.Entities.Professor>()
                .Select(professor => professorMapper.map(professor));
        }
    }
}