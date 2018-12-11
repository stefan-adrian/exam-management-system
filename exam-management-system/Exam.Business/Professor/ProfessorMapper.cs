using AutoMapper;

namespace Exam.Business.Professor
{
    public class ProfessorMapper
    {
        private readonly IMapper autoMapper;

        public ProfessorMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<ProfessorDetailsDto, Domain.Entities.Professor>(); }).CreateMapper();
        }

        public Domain.Entities.Professor map(ProfessorCreatingDto professorCreatingDto)
        {
            Domain.Entities.Professor professor = new Domain.Entities.Professor(
                professorCreatingDto.RegistrationNumber,
                professorCreatingDto.Email,
                professorCreatingDto.Password,
                professorCreatingDto.FirstName,
                professorCreatingDto.LastName);
            return professor;
        }

        public ProfessorDetailsDto map(Domain.Entities.Professor professor)
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

        public Domain.Entities.Professor map(ProfessorDetailsDto professorDetails, Domain.Entities.Professor professor)
        {
            this.autoMapper.Map(professorDetails, professor);
            return professor;
        }
    }
}
