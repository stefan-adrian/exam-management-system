using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Business.Student.Exception;
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
            this.writeRepository = writeRepository ?? throw new ArgumentNullException();
            this.readRepository = readRepository ?? throw new ArgumentNullException();
            this.studentMapper = studentMapper ?? throw new ArgumentNullException();
        }

        public async Task<List<StudentDetailsDto>> GetAll()
        {
            return await readRepository.GetAll<Domain.Entities.Student>()
                .Select(student => studentMapper.Map(student)).ToListAsync();
        }

        public async Task<Domain.Entities.Student> GetStudentById(Guid id)
        {
            var student = await readRepository.GetByIdAsync<Domain.Entities.Student>(id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            return student;
        }

        public async Task<StudentDetailsDto> GetDetailsDtoById(Guid id)
        {
            var student = await GetStudentById(id);
            return studentMapper.Map(student);
        }


        public async Task<StudentDetailsDto> Create(StudentCreationDto studentCreationDto)
        {
            Domain.Entities.Student student = studentMapper.Map(studentCreationDto);
            await writeRepository.AddNewAsync(student);
            await writeRepository.SaveAsync();
            return studentMapper.Map(student);
        }

        public async Task<StudentDetailsDto> Update(Guid id, StudentCreationDto studentCreationDto)
        {
            StudentDetailsDto studentDetailsDto = studentMapper.Map(id, studentCreationDto);
            var student = GetStudentById(id).Result;
            writeRepository.Update(studentMapper.Map(studentDetailsDto, student));
            await writeRepository.SaveAsync();
            return studentDetailsDto;
        }


        public async Task Delete(Guid id)
        {
            var student = GetStudentById(id).Result;
            writeRepository.Delete(student);
            await writeRepository.SaveAsync();
        }
    }
}
