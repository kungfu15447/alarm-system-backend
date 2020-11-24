using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.AlarmLog
{
    public class CreateAlarmLogTest
    {
        [Fact]
        public void TestServiceShouldCallRepoOnce()
        {
            //Given
            var mockRepo = new Mock<IAlarmLogRepository>();
            var service = new AlarmLogService(mockRepo.Object);
            
            var al = new AlarmSystem.Core.Entity.Dto.AlarmLog();

            //When
            mockRepo.Setup(mr => mr.AddAlarmLog(It.IsAny<AlarmSystem.Core.Entity.Dto.AlarmLog>()));

            service.CreateAlarmLog(al);

            //Then
            mockRepo.Verify(mr => mr.AddAlarmLog(It.IsAny<AlarmSystem.Core.Entity.Dto.AlarmLog>()), Times.Once);
        }
    }
}