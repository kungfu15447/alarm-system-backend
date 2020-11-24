using System.Collections.Generic;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.AlarmLog
{
    public class GetAlarmLogTest
    {
        [Fact]
        public void TestRepoShouldReturnAlarmLogs() {
            
            //Given
            var mockRepo = new Mock<IAlarmLogRepository>();
            var service = new AlarmLogService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.GetAlarmLog()).Returns(It.IsAny<List<AlarmSystem.Core.Entity.DB.AlarmLog>>());

            service.GetAlarmLog();
        
            //Then
            mockRepo.Verify(mr => mr.GetAlarmLog(), Times.Once);
        }
    }
}