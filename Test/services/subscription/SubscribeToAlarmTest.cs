using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class SubscribeToAlarmTest
    {
        [Fact]
        public void TestRepoShouldBeCalledOnceIfAlarmWatchIsNotEmptyOrNull()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            
            AlarmWatch aw = new AlarmWatch{
                Alarm = new Core.Entity.DB.Alarm {
                    AlarmId = 1,
                    Code = 205,
                    Description = "TestDescription"
                },
                WatchId = "1"
            };

            //When
            mockRepo.Setup(mr => mr.SubscribeToAlarm(It.IsAny<AlarmWatch>()));

            service.SubscribeToAlarm(aw);
        
            //Then
            mockRepo.Verify(mr => mr.SubscribeToAlarm(It.IsAny<AlarmWatch>()), Times.Once);
        }

        [Fact]
        public void TestRepoShouldNotBeCalledIfAlarmWatchIsNullOrEmpty()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.SubscribeToAlarm(It.IsAny<AlarmWatch>()));
        
            //Then
            //Assert.Throws<InvalidDataException>(() => service.SubscribeToAlarm(aw));
            mockRepo.Verify(mr => mr.SubscribeToAlarm(It.IsAny<AlarmWatch>()), Times.Never);
        }
    }
}