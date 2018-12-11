using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ProfessorDetailsDto>> GetAll()
        {
            return await GetAllProfessorDetailsDtos().ToListAsync();
        }

        public async Task<ProfessorDetailsDto> GetById(Guid id)
        {
            var professor = await this.readRepository.GetByIdAsync<Domain.Entities.Professor>(id);
            return this.professorMapper.map(professor);
        }

        private IQueryable<ProfessorDetailsDto> GetAllProfessorDetailsDtos()
        {
            return this.readRepository.GetAll<Domain.Entities.Professor>()
                .Select(professor => this.professorMapper.map(professor));
        }

        public async Task<ProfessorDetailsDto> Create(ProfessorCreatingDto newProfessor)
        {
            Domain.Entities.Professor professor = this.professorMapper.map(newProfessor);
            await this.writeRepository.AddNewAsync(professor);
            await this.writeRepository.SaveAsync();
            return this.professorMapper.map(professor);
        }

        public async Task<ProfessorDetailsDto> Update(ProfessorDetailsDto existingProfessor)
        {
            var professor = this.readRepository.GetByIdAsync<Domain.Entities.Professor>(existingProfessor.Id).Result;
            this.writeRepository.Update(this.professorMapper.map(existingProfessor, professor));
            await this.writeRepository.SaveAsync();
            return existingProfessor;
        }

        public async Task Delete(ProfessorDetailsDto existingProfessor)
        {
            var professor = this.readRepository.GetByIdAsync<Domain.Entities.Professor>(existingProfessor.Id).Result;
            this.writeRepository.Delete(professor);
            await this.writeRepository.SaveAsync();
        }
    }
}