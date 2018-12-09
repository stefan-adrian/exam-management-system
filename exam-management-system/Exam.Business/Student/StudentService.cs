using Exam.Domain.Interfaces;

namespace Exam.Business.Student
{
    public class StudentService : IStudentService
    {
        private readonly IReadRepository readRepository;

        private readonly IWriteRepository writeRepository;

        public StudentService(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
            this.readRepository = readRepository;
        }
    }
}
