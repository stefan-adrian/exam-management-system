using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Business.Professor
{
    public class ProfessorMapper
    {
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
            professorDetailsDto.Id = professorDetailsDto.Id;
            professorDetailsDto.RegistrationNumber = professorDetailsDto.RegistrationNumber;
            professorDetailsDto.Email = professorDetailsDto.Email;
            professorDetailsDto.Password = professorDetailsDto.Password;
            professorDetailsDto.FirstName = professorDetailsDto.FirstName;
            professorDetailsDto.LastName = professorDetailsDto.LastName;
            return professorDetailsDto;
        }
    }
}
