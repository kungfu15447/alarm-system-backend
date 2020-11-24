using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class GetAlarmSubscriptionsByWatchTest
    {
        [Fact]
        public void TestRepoShouldBeCalledOnceIfWatchIdIsNotEmptyOrNull()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            var watchId = "watch-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadAllAlarmSubscriptionsByWatch(It.IsAny<string>())).Returns(It.IsAny<List<AlarmWatch>>());

            service.GetAlarmSubscriptionsFromWatch(watchId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAllAlarmSubscriptionsByWatch(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestRepoShouldNotBeCalledIfWatchIdIsNullOrEmpty(string watchId)
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.ReadAllAlarmSubscriptionsByWatch(It.IsAny<string>())).Returns(It.IsAny<List<AlarmWatch>>());
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetAlarmSubscriptionsFromWatch(watchId));
            mockRepo.Verify(mr => mr.ReadAllAlarmSubscriptionsByWatch(It.IsAny<string>()), Times.Never);
        }
    }
}