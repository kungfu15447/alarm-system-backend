using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Alarm
{
    public class GetAlarmsWithSubsTest
    {
        [Fact]
        public void MethodShouldCallGetMachinesWithSubsMethodFromRepoOnce()
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);
            var watchId = "watch-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadAllAlarmsWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<AlarmWithSubscription>>());

            service.GetAllAlarmsWithSubs(watchId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAllAlarmsWithSubs(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestRepoShouldNotBeCalledIfWatchIdIsNullOrEmpty(string watchId)
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.ReadAllAlarmsWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<AlarmWithSubscription>>());
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetAllAlarmsWithSubs(It.IsAny<string>()));
            mockRepo.Verify(mr => mr.ReadAllAlarmsWithSubs(It.IsAny<string>()), Times.Never);
        }
    }
}