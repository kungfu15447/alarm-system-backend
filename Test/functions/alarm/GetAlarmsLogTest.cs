
using AlarmSystem.Functions.Machine;
using AlarmSystem.Test.Utils;
using core.application;
using functions.alarm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.alarm
{
    public class GetAlarmsLogTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallAlarmServiceOnce() {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmService = new Mock<IAlarmService>();

            //When
            alarmService.Setup(ms => ms.GetAlarmLog());

            var res = await new GetAlarmLog(alarmService.Object).Run(req, logger);

            //Then
            alarmService.Verify(ms => ms.GetAlarmLog(), Times.Once());
            
        }

        [Fact]
        public async void FunctionShouldReturnOkObjectResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmService = new Mock<IAlarmService>();

            //When
            var res = (OkObjectResult)await new GetAlarmLog(alarmService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }
    }
}