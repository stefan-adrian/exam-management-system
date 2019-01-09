using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Classroom.Exception;
using Exam.Business.Exam.Exception;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
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
            exam.ClassroomAllocation.Add(classroomAllocation);
            await writeRepository.SaveAsync();
            return classroomAllocationMapper.Map(classroomAllocation);
        }

        public async Task<List<ClassroomAllocationDetailsDto>> GetByExam(Guid examId)
        {
            var exam = await readRepository.GetAll<Domain.Entities.Exam>().Where(e => e.Id == examId)
                .Include(e => e.ClassroomAllocation).ThenInclude(ca => ca.Classroom)
                .Include(e => e.ClassroomAllocation).ThenInclude(ca => ca.Exam)
                .FirstOrDefaultAsync();

            return exam.ClassroomAllocation.Select(c => classroomAllocationMapper.Map(c)).ToList();
        }
    }
}
