using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Domain.Entities;
using Exam.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exam.Business.Student
{
    public class StudentService : IStudentService
    {
        private readonly IReadRepository readRepository;
        private readonly IWriteRepository writeRepository;
        private readonly IStudentMapper studentMapper;

        public StudentService(IReadRepository readRepository, IWriteRepository writeRepository, IStudentMapper studentMapper)
        {
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
            this.studentMapper = studentMapper;
        }

        public async Task<List<StudentDetailsDto>> GetAll()
        {
            return await readRepository.GetAll<Domain.Entities.Student>()
                .Select(student => studentMapper.Map(student)).ToListAsync();
        }

        public async Task<StudentDetailsDto> GetById(Guid id)
        {
            var student = await readRepository.GetByIdAsync<Domain.Entities.Student>(id);
            return studentMapper.Map(student);
        }


        public async Task<StudentDetailsDto> Create(StudentCreationDto studentCreationDto)
        {
            Domain.Entities.Student student = studentMapper.Map(studentCreationDto);
            await writeRepository.AddNewAsync(student);
            await writeRepository.SaveAsync();
            return studentMapper.Map(student);
        }

        public async Task Delete(Guid id)
        {
            var student = readRepository.GetByIdAsync<Domain.Entities.Student>(id).Result;
            writeRepository.Delete(student);
            await writeRepository.SaveAsync();
        }
    }
}
