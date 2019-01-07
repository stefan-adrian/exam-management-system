using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Classroom.Exception;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Classroom
{
    public class ClassroomService : IClassroomService
    {
        private IWriteRepository writeRepository;
        private IReadRepository readRepository;
        private IClassroomMapper classroomMapper;

        public ClassroomService(IWriteRepository writeRepository, IReadRepository readRepository, IClassroomMapper classroomMapper)
        {
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.classroomMapper = classroomMapper ?? throw new ArgumentNullException();
        }

        public async Task<List<ClassroomDetailsDto>> GetAll()
        {
            return await this.readRepository.GetAll<Domain.Entities.Classroom>()
                .Select(classroom => classroomMapper.Map(classroom)).ToListAsync();
        }

        public async Task<ClassroomDetailsDto> GetDetailsDtoById(Guid id)
        {
            var classroom = await this.readRepository.GetByIdAsync<Domain.Entities.Classroom>(id);
            if (classroom == null)
            {
                new ClassroomNotFoundException(id);
            }

            return this.classroomMapper.Map(classroom);
        }

        public async Task<ClassroomDetailsDto> Create(ClassroomCreatingDto classroomCreatingDto)
        {
            var classroom = this.classroomMapper.Map(classroomCreatingDto);
            await this.writeRepository.AddNewAsync(classroom);
            await this.writeRepository.SaveAsync();
            return this.classroomMapper.Map(classroom);
        }
    }
}
