using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class GetMachineSubscriptionsByMachineTest
    {
        [Fact]
        public void TestRepoShouldBeCalledOnceIfMachineIdIsNotEmptyOrNull()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            var machineId = "machine-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadAllMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(It.IsAny<List<MachineWatch>>());
            service.GetMachineSubscriptionsByMachine(machineId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAllMachineSubscriptionsByMachine(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestRepoShouldNotBeCalledIfMachineIdIsNullOrEmpty(string machineId)
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.ReadAllMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(It.IsAny<List<MachineWatch>>());
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetMachineSubscriptionsByMachine(machineId));
            mockRepo.Verify(mr => mr.ReadAllMachineSubscriptionsByWatch(It.IsAny<string>()), Times.Never);
        }
    }
}