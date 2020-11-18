using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.TestAlarm
{
    public class GetAlarmByCodeTest
    {
        [Fact]
        public void TestRepoMethodShouldBeCalledOnce()
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);
            var alarmCode = 1;
        
            //When
            mockRepo.Setup(mr => mr.ReadAlarmByCode(It.IsAny<int>())).Returns(new AlarmSystem.Core.Entity.Dto.Alarm());

            service.GetAlarmByCode(alarmCode);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAlarmByCode(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestMethodShouldThrowExceptionIfAlarmWasNotFound()
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);
            var alarmCode = 1;
        
            //When
            mockRepo.Setup(mr => mr.ReadAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<AlarmSystem.Core.Entity.Dto.Alarm>());
        
            //Then
            Assert.Throws<EntityNotFoundException>(() => service.GetAlarmByCode(alarmCode));
            mockRepo.Verify(mr => mr.ReadAlarmByCode(It.IsAny<int>()), Times.Once);
        }
    }
}