using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Infrastructure.Repositories;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;
using System.IO;

namespace AlarmSystem.Test.Services.Machine
{
    public class CreateMachineTest
    {
        [Fact]
        public void MethodShouldCallCreateMachineMethodFromRepoOnce()
        {
            //Given
            var machineRepo = new Mock<IMachineRepository>();
            var machine = new AlarmSystem.Core.Entity.DB.Machine
            {
                MachineId = "machine-id-test",
                Name = "machine-name-test",
                Type = "machine-type-test"
            };

            //When
            machineRepo.Setup(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()));

            var machineService = new MachineService(machineRepo.Object);

            machineService.CreateMachine(machine);
            //Then
            machineRepo.Verify(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()), Times.Once);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(null, "")]
        public void MethodShouldNotCallMachineRepoAndThrowExceptionIfDataIsInvalid(string name, string type)
        {
            //Given
            var machineRepo = new Mock<IMachineRepository>();
            var machine = new AlarmSystem.Core.Entity.DB.Machine
            {
                MachineId = "machine-id-test",
                Name = name,
                Type = type
            };

            //When
            machineRepo.Setup(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()));

            var machineService = new MachineService(machineRepo.Object);

            
            //Then
            Assert.Throws<InvalidDataException>(() => machineService.CreateMachine(machine));
            machineRepo.Verify(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()), Times.Never);
        }

        [Fact]
        public void MethodShoouldNotCallRepoMethodAndThrowInvalidDataExceptionIfMachineIsNull()
        {
            //Given
            var machineRepo = new Mock<IMachineRepository>();

            //When
            machineRepo.Setup(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()));

            var machineService = new MachineService(machineRepo.Object);

            
            //Then
            Assert.Throws<InvalidDataException>(() => machineService.CreateMachine(null));
            machineRepo.Verify(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()), Times.Never);
        }
    }
}