
using AlarmSystem.Core.Application;
using AlarmSystem.Functions.AlarmLog;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.AlarmLog
{
    public class GetAlarmLogsTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallAlarmLogServiceOnce() {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmLogService = new Mock<IAlarmLogService>();

            //When
            alarmLogService.Setup(ms => ms.GetAlarmLog());

            var res = await new GetAlarmLogs(alarmLogService.Object).Run(req, logger);

            //Then
            alarmLogService.Verify(ms => ms.GetAlarmLog(), Times.Once());
            
        }

        [Fact]
        public async void FunctionShouldReturnOkObjectResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmLogService = new Mock<IAlarmLogService>();

            //When
            var res = (OkObjectResult)await new GetAlarmLogs(alarmLogService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }
    }
}