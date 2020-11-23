using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class DeleteMachineSubscriptionFromWatchTest
    {
        [Fact]
        public void TestServiceShouldCallRepoMethodOnce()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            
            //When
            mockRepo.Setup(mr => mr.RemoveMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()));

            service.DeleteMachineSubscriptionFromWatch(new MachineWatch());

            //Then
            mockRepo.Verify(mr => mr.RemoveMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()), Times.Once);
        }
    }
}