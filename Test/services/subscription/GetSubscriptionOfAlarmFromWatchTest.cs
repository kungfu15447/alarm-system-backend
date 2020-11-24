using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class GetSubscriptionOfAlarmFromWatchTest
    {
        [Fact]
        public void TestServiceShouldCallRepoMethodOnce()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);

            int alarmId = 1;
            string watchId = "watch-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>())).Returns(new AlarmWatch());
            service.GetSubscriptionOfAlarmFromWatch(alarmId, watchId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestServiceShouldThrowExceptionIfWatchIdIsNullOrEmpty(string watchId)
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            int alarmId = 1;

            //When
            mockRepo.Setup(mr => mr.ReadSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>())).Returns(new AlarmWatch());
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetSubscriptionOfAlarmFromWatch(alarmId, watchId));
            mockRepo.Verify(mr => mr.ReadSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
    }
}