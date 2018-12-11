using System;

namespace Exam.Business.Professor
{
    public interface IProfessorMapper
    {
        ProfessorDetailsDto Map(Guid professorId, ProfessorCreatingDto professorCreatingDto);

        ProfessorDetailsDto Map(Domain.Entities.Professor professor);

        Domain.Entities.Professor Map(ProfessorCreatingDto professorCreatingDto);

        Domain.Entities.Professor Map(ProfessorDetailsDto professorDetails, Domain.Entities.Professor professor);
    }
}