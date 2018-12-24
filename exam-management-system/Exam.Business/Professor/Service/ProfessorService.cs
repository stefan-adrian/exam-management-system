using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Professor.Exception;
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
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.professorMapper = professorMapper ?? throw new ArgumentNullException();
        }

        public async Task<List<ProfessorDetailsDto>> GetAll()
        {
            return await GetAllProfessorDetailsDtos().ToListAsync();
        }

        public async Task<Domain.Entities.Professor> GetProfessorById(Guid id)
        {
            var professor = await this.readRepository.GetByIdAsync<Domain.Entities.Professor>(id);
            if (professor == null)
            {
                throw new ProfessorNotFoundException(id);
            }

            return professor;
        }

        public async Task<ProfessorDetailsDto> GetById(Guid id)
        {
            var professor = await GetProfessorById(id);
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
            if (professor == null)
            {
                throw new ProfessorNotFoundException(existingProfessorId);
            }
            this.writeRepository.Update(this.professorMapper.Map(professorDetailsDto, professor));
            await this.writeRepository.SaveAsync();
            return professorDetailsDto;
        }

        public async Task Delete(Guid existingProfessorId)
        {
            var professor = this.readRepository.GetByIdAsync<Domain.Entities.Professor>(existingProfessorId).Result;
            if (professor == null)
            {
                throw new ProfessorNotFoundException(existingProfessorId);
            }
            this.writeRepository.Delete(professor);
            await this.writeRepository.SaveAsync();
        }
    }
}