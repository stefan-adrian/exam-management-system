using System;

namespace Exam.Business.Professor
{
    public interface IProfessorMapper
    {
        ProfessorDetailsDto map(Guid professorId, ProfessorCreatingDto professorCreatingDto);

        ProfessorDetailsDto map(Domain.Entities.Professor professor);

        Domain.Entities.Professor map(ProfessorCreatingDto professorCreatingDto);

        Domain.Entities.Professor map(ProfessorDetailsDto professorDetails, Domain.Entities.Professor professor);
    }
}