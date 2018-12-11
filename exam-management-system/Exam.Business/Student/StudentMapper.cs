using System;
using AutoMapper;

namespace Exam.Business.Student
{
    public class StudentMapper : IStudentMapper
    {
        private readonly IMapper autoMapper;

        public StudentMapper()
        {
            autoMapper = new MapperConfiguration(cfg => { cfg.CreateMap<StudentDetailsDto, Domain.Entities.Student>(); }).CreateMapper();
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

        public StudentDetailsDto Map(Domain.Entities.Student student)
        {
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto();
            studentDetailsDto.Id = student.Id;
            studentDetailsDto.FirstName = student.FirstName;
            studentDetailsDto.LastName = student.LastName;
            studentDetailsDto.Email = student.Email;
            studentDetailsDto.RegistrationNumber = student.RegistrationNumber;
            studentDetailsDto.YearOfStudy = student.YearOfStudy;
            studentDetailsDto.Password = student.Password;
            return studentDetailsDto;
        }

        public StudentDetailsDto Map(Guid id,StudentCreationDto studentCreationDto)
        {
            StudentDetailsDto studentDetailsDto = new StudentDetailsDto();
            studentDetailsDto.Id = id;
            studentDetailsDto.FirstName = studentCreationDto.FirstName;
            studentDetailsDto.LastName = studentCreationDto.LastName;
            studentDetailsDto.Email = studentCreationDto.Email;
            studentDetailsDto.RegistrationNumber = studentCreationDto.RegistrationNumber;
            studentDetailsDto.YearOfStudy = studentCreationDto.YearOfStudy;
            studentDetailsDto.Password = studentCreationDto.Password;
            return studentDetailsDto;
        }

        public Domain.Entities.Student map(StudentDetailsDto studentDetails, Domain.Entities.Student student)
        {
            autoMapper.Map(studentDetails, student);
            return student;
        }
    }
}
