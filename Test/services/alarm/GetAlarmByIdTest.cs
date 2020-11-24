using System.IO;
using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Alarm
{
    public class GetAlarmByIdTest
    {
        [Fact]
        public void TestRepoShouldBeCalledIfAlarmIdIsNotEmptyOrNull() {
            
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);
            int alarmId = 1;
            AlarmSystem.Core.Entity.Dto.Alarm alarm = new AlarmSystem.Core.Entity.Dto.Alarm() {
                AlarmId = alarmId,
                Code = 123,
                Description = "MockDescription"
            };
            //When
            mockRepo.Setup(mr => mr.ReadAlarmById(alarmId)).Returns(alarm);

            service.GetAlarmById(alarmId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAlarmById(alarmId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TestRepoShouldNotBeCalledIfAlarmIdIsNotValid(int alarmId)
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.ReadAlarmById(It.IsAny<int>())).Returns(It.IsAny<AlarmSystem.Core.Entity.Dto.Alarm>());
        

            //Then
            Assert.Throws<InvalidDataException>(() => service.GetAlarmById(alarmId));
            mockRepo.Verify(mr => mr.ReadAlarmById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void TestRepoShouldThrowEntityNotFoundExceptionIfAlarmIsNull()
        {
            //Given
            var mockRepo = new Mock<IAlarmRepository>();
            var service = new AlarmService(mockRepo.Object);

            var alarmId = 1;

            //When
            mockRepo.Setup(mr => mr.ReadAlarmById(It.IsAny<int>())).Returns(It.IsAny<AlarmSystem.Core.Entity.Dto.Alarm>());
        

            //Then
            Assert.Throws<EntityNotFoundException>(() => service.GetAlarmById(alarmId));
        }
    }
}