using System.Collections.Generic;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class GetAlarmSubscriptionsByAlarmCodeTest
    {
        [Fact]
        public void TestMethodShouldCallRepoMethodOnce()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            var alarmCode = 42;

            //When
            mockRepo.Setup(mr => mr.ReadAllAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(It.IsAny<List<AlarmWatch>>());
            service.GetAlarmSubscriptionsByAlarmCode(alarmCode);
            //Then
            mockRepo.Verify(mr => mr.ReadAllAlarmSubscriptionsByAlarmCode(It.IsAny<int>()), Times.Once);
        }
    }
}