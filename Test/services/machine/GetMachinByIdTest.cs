using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Machine
{
    public class GetMachineByIdTest
    {
        [Fact]
        public void TestServiceShouldCallRepoOnce()
        {
            //Given
            var mockRepo = new Mock<IMachineRepository>();

            var service = new MachineService(mockRepo.Object);

            var machineId = "machine-id-1";

            var machine = new AlarmSystem.Core.Entity.Dto.Machine()
            {
                MachineId = machineId
            };
            
            //When
            mockRepo.Setup(mr => mr.ReadMachineById(It.IsAny<string>())).Returns(machine);

            service.GetMachineById(machineId);

            //Then
            mockRepo.Verify(mr => mr.ReadMachineById(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void TestServiceShouldThrowNotFoundExceptionIfMachineIsNull()
        {
            //Given
            var mockRepo = new Mock<IMachineRepository>();

            var service = new MachineService(mockRepo.Object);

            var machineId = "machine-id-1";

            var machine = new AlarmSystem.Core.Entity.Dto.Machine()
            {
                MachineId = machineId
            };
            
            //When
            mockRepo.Setup(mr => mr.ReadMachineById(It.IsAny<string>())).Returns(It.IsAny<AlarmSystem.Core.Entity.Dto.Machine>());

            //Then
            Assert.Throws<EntityNotFoundException>(() => service.GetMachineById(machineId));
        }
    }
}