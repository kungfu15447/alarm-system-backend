using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Domain;
using AlarmSystem.Functions.Machine;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Machine
{
    public class CreateMachineTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallMachineServiceOnce()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var machineService = new Mock<IMachineService>();

            //When
            machineService.Setup(ms => ms.CreateMachine());

            var res = await new CreateMachine(machineService.Object).Run(req, logger);

            //Then
            machineService.Verify(ms => ms.CreateMachine(), Times.Once());
        
        }

        [Fact]
        public async void FucntionShouldReturnOkResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var machineService = new Mock<IMachineService>();

            //When
            var res = (OkResult)await new CreateMachine(machineService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }
    }
}