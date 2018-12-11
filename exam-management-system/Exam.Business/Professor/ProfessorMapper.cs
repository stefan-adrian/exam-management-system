using System;
using AutoMapper;

namespace Exam.Business.Professor
{
    public class ProfessorMapper : IProfessorMapper
    {
        private readonly IMapper autoMapper;

        public ProfessorMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<ProfessorDetailsDto, Domain.Entities.Professor>(); }).CreateMapper();
        }

        public ProfessorDetailsDto Map(Domain.Entities.Professor professor)
        {
            ProfessorDetailsDto professorDetailsDto = new ProfessorDetailsDto();
            professorDetailsDto.Id = professor.Id;
            professorDetailsDto.RegistrationNumber = professor.RegistrationNumber;
            professorDetailsDto.Email = professor.Email;
            professorDetailsDto.Password = professor.Password;
            professorDetailsDto.FirstName = professor.FirstName;
            professorDetailsDto.LastName = professor.LastName;
            return professorDetailsDto;
        }

        public ProfessorDetailsDto Map(Guid professorId, ProfessorCreatingDto professorCreatingDto)
        {
            ProfessorDetailsDto professorDetailsDto = new ProfessorDetailsDto();
            professorDetailsDto.Id = professorId;
            professorDetailsDto.RegistrationNumber = professorCreatingDto.RegistrationNumber;
            professorDetailsDto.Email = professorCreatingDto.Email;
            professorDetailsDto.Password = professorCreatingDto.Password;
            professorDetailsDto.FirstName = professorCreatingDto.FirstName;
            professorDetailsDto.LastName = professorCreatingDto.LastName;
            return professorDetailsDto;
        }

        public Domain.Entities.Professor Map(ProfessorCreatingDto professorCreatingDto)
        {
            Domain.Entities.Professor professor = new Domain.Entities.Professor(
                professorCreatingDto.RegistrationNumber,
                professorCreatingDto.Email,
                professorCreatingDto.Password,
                professorCreatingDto.FirstName,
                professorCreatingDto.LastName);
            return professor;
        }

        public Domain.Entities.Professor Map(ProfessorDetailsDto professorDetails, Domain.Entities.Professor professor)
        {
            this.autoMapper.Map(professorDetails, professor);
            return professor;
        }
    }
}
