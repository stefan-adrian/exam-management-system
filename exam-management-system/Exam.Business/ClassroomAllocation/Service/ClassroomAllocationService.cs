using System;
using System.Threading.Tasks;
using Exam.Business.Classroom.Exception;
using Exam.Business.ClassroomAllocation.Exception;
using Exam.Business.Exam.Exception;
using Exam.Business.Student.Exception;
using Exam.Domain.Interfaces;

namespace Exam.Business.ClassroomAllocation
{
    public class ClassroomAllocationService : IClassroomAllocationService
    {
        private readonly IClassroomAllocationMapper classroomAllocationMapper;
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;

        public ClassroomAllocationService(IClassroomAllocationMapper classroomAllocationMapper, IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this.classroomAllocationMapper = classroomAllocationMapper ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException(); ;
            this.writeRepository = writeRepository ?? throw new ArgumentNullException(); ;
        }

        public async Task<ClassroomAllocationDetailsDto> Create(ClassroomAllocationCreatingDto classroomAllocationCreatingDto)
        {
            var exam = await readRepository.GetByIdAsync<Domain.Entities.Exam>(classroomAllocationCreatingDto.ExamId);

            if (exam == null)
            {
                throw new ExamNotFoundException(classroomAllocationCreatingDto.ExamId);
            }

            var classroom = await readRepository.GetByIdAsync<Domain.Entities.Classroom>(classroomAllocationCreatingDto.ClassroomId);

            if (classroom == null)
            {
                throw new ClassroomNotFoundException(classroomAllocationCreatingDto.ClassroomId);
            }

            var classroomAllocation = classroomAllocationMapper.Map(exam, classroom);
            await writeRepository.AddNewAsync(classroomAllocation);
            await writeRepository.SaveAsync();
            return classroomAllocationMapper.Map(classroomAllocation);
        }

        public Task<ClassroomAllocationDetailsDto> GetByExam(Guid examId)
        {
            throw new NotImplementedException();
        }

        public async Task CheckIn(Guid classroomAllocationId, Guid studentId)
        {
            var classroomAllocation = await readRepository.GetByIdAsync<Domain.Entities.ClassroomAllocation>(classroomAllocationId);

            if (classroomAllocation == null)
            {
                throw new ClassroomAllocationNotFound(classroomAllocationId);
            }

            var student = await readRepository.GetByIdAsync<Domain.Entities.Student>(studentId);

            if (student == null)
            {
                throw new StudentNotFoundException(studentId);
            }

            classroomAllocation.CheckedInStudents.Add(student);
            await writeRepository.SaveAsync();
        }
    }
}
