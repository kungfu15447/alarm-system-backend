using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class DeleteAlarmSubscriptionFromWatchTest
    {
        [Fact]
        public void TestServiceShouldCallRepoMethodOnce()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
        
            //When
            mockRepo.Setup(mr => mr.RemoveAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()));
            service.DeleteAlarmSubscriptionFromWatch(new AlarmWatch());
        
            //Then
            mockRepo.Verify(mr => mr.RemoveAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()));
        }
    }
}