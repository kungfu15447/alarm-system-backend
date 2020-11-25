using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Infrastructure.Repositories;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Machine
{
    public class CreateMachineTest
    {
        [Fact]
        public void MethodShouldCallCreateMachineMethodFromRepoOnce()
        {
            //Given
            var machineRepo = new Mock<IMachineRepository>();

            //When
            machineRepo.Setup(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()));

            var machineService = new MachineService(machineRepo.Object);

            machineService.CreateMachine();
            //Then
            machineRepo.Verify(mr => mr.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()), Times.Once);
        }
    }
}