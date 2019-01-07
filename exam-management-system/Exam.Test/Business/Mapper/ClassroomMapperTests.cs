using System;
using Exam.Business.Classroom;
using Exam.Domain.Entities;
using Exam.Test.TestUtils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exam.Test.Business.Mapper
{
    [TestClass]
    public class ClassroomMapperTests
    {
        private Classroom _classroom;
        private ClassroomCreatingDto _classroomCreatingDto;
        private ClassroomDetailsDto _classroomDetailsDto;
        private IClassroomMapper _classroomMapper;

        [TestInitialize]
        public void Setup()
        {
            this._classroom = ClassroomTestUtils.GetClassroom();
            this._classroomCreatingDto = ClassroomTestUtils.GetClassroomCreatingDto();
            this._classroomDetailsDto = ClassroomTestUtils.GetClassroomDetailsDto(_classroom.Id);
            this._classroomMapper = new ClassroomMapper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this._classroom = null;
            this._classroomCreatingDto = null;
            this._classroomDetailsDto = null;
            this._classroomMapper = null;
        }

        [TestMethod]
        public void Map_ShouldReturnClassroomDetailsDto_WhenArgumentIsClassroom()
        {
            // Act
            var result = this._classroomMapper.Map(this._classroom);
            // Assert
            result.Should().BeEquivalentTo(this._classroomDetailsDto);
        }

        [TestMethod]
        public void Map_ShouldReturnClassroom_WhenArgumentIsClassroomCreatingDto()
        {
            // Act
            var result = this._classroomMapper.Map(this._classroomCreatingDto);
            // Assert
            result.Should().Match<Classroom>((obj) =>
                obj.Capacity == this._classroomCreatingDto.Capacity &&
                obj.Location == this._classroomCreatingDto.Location);
        }
    }
}
