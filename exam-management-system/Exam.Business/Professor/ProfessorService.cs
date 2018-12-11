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

        private readonly IProfessorMapper professorMapper;

        public ProfessorService(IReadRepository readRepository, IWriteRepository writeRepository,
            IProfessorMapper professorMapper)
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
            return this.professorMapper.Map(professor);
        }

        private IQueryable<ProfessorDetailsDto> GetAllProfessorDetailsDtos()
        {
            return this.readRepository.GetAll<Domain.Entities.Professor>()
                .Select(professor => this.professorMapper.Map(professor));
        }

        public async Task<ProfessorDetailsDto> Create(ProfessorCreatingDto newProfessor)
        {
            Domain.Entities.Professor professor = this.professorMapper.Map(newProfessor);
            await this.writeRepository.AddNewAsync(professor);
            await this.writeRepository.SaveAsync();
            return this.professorMapper.Map(professor);
        }

        public async Task<ProfessorDetailsDto> Update(Guid existingProfessorId, ProfessorCreatingDto professorCreatingDto)
        {
            ProfessorDetailsDto professorDetailsDto = this.professorMapper.Map(existingProfessorId, professorCreatingDto);
            var professor = this.readRepository.GetByIdAsync<Domain.Entities.Professor>(existingProfessorId).Result;
            this.writeRepository.Update(this.professorMapper.Map(professorDetailsDto, professor));
            await this.writeRepository.SaveAsync();
            return professorDetailsDto;
        }

        public async Task Delete(Guid existingProfessorId)
        {
            var professor = this.readRepository.GetByIdAsync<Domain.Entities.Professor>(existingProfessorId).Result;
            this.writeRepository.Delete(professor);
            await this.writeRepository.SaveAsync();
        }
    }
}