using System;
using AutoMapper;

namespace Exam.Business.Student
{
    public class StudentMapper : IStudentMapper
    {
        private readonly IMapper autoMapper;

        public StudentMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<StudentDetailsDto, Domain.Entities.Student>(); })
                .CreateMapper();
        }

        public StudentDetailsDto Map(Domain.Entities.Student student)
        {
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                RegistrationNumber = student.RegistrationNumber,
                YearOfStudy = student.YearOfStudy,
                Password = student.Password
            };
            return studentDetailsDto;
        }

        public StudentDetailsDto Map(Guid id, StudentCreationDto studentCreationDto)
        {
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto
            {
                Id = id,
                FirstName = studentCreationDto.FirstName,
                LastName = studentCreationDto.LastName,
                Email = studentCreationDto.Email,
                RegistrationNumber = studentCreationDto.RegistrationNumber,
                YearOfStudy = studentCreationDto.YearOfStudy,
                Password = studentCreationDto.Password
            };
            return studentDetailsDto;
        }

        public Domain.Entities.Student Map(StudentCreationDto studentCreationDto)
        {
            return new Domain.Entities.Student(
                studentCreationDto.RegistrationNumber,
                studentCreationDto.Email,
                studentCreationDto.Password,
                studentCreationDto.FirstName,
                studentCreationDto.LastName,
                studentCreationDto.YearOfStudy);
        }

        public Domain.Entities.Student Map(StudentDetailsDto studentDetails, Domain.Entities.Student student)
        {
            autoMapper.Map(studentDetails, student);
            return student;
        }
    }
}
