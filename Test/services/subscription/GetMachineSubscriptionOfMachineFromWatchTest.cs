using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class GetMachineSubscriptionOfMachineFromWatch
    {
        [Fact]
        public void TestServiceShouldCallRepoOnceIfParametersAreNotEmptyOrNull()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();

            var service = new WatchService(mockRepo.Object);

            var machineId = "machine-id-1";
            var watchId = "watch-id-1";

            var mw = new MachineWatch();

            //When
            mockRepo.Setup(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>())).Returns(mw);

            service.GetMachineSubcriptionOfMachineFromWatch(machineId, watchId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData("machine-id-1", "")]
        [InlineData("machine-id-1", null)]
        [InlineData("", "watch-id-1")]
        [InlineData(null, "watch-id-1")]
        public void TestServiceShouldThrowErrorIfParametersAreEmptyOrNull(string machineId, string watchId)
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();

            var service = new WatchService(mockRepo.Object);

            var mw = new MachineWatch();

            //When
            mockRepo.Setup(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>())).Returns(mw);
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetMachineSubcriptionOfMachineFromWatch(machineId, watchId));
            mockRepo.Verify(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void TestServiceShouldThrowErrorIfMachineWatchIsNotFound()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();

            var service = new WatchService(mockRepo.Object);

            var machineId = "machine-id-1";
            var watchId = "watch-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<MachineWatch>);
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetMachineSubcriptionOfMachineFromWatch(machineId, watchId));
            mockRepo.Verify(mr => mr.ReadMachineSubscriptionOfMachineByWatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}