using System.Collections.Generic;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Machine
{
    public class GetMachinesTest
    {
        [Fact]
        public void ReadAllMachineFromMachineRepoShouldBeCalledOnce()
        {
            //Given
            var machineRepo = new Mock<IMachineRepository>();

            //When
            machineRepo.Setup(mr => mr.ReadAllMachines()).Returns(It.IsAny<List<AlarmSystem.Core.Entity.DB.Machine>>());
            var machineService = new MachineService(machineRepo.Object);

            machineService.GetMachines();

            //Then
            machineRepo.Verify(mr => mr.ReadAllMachines(), Times.Once);
        }
    }
}