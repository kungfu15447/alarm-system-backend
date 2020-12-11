using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Domain;
using AlarmSystem.Functions.Machine.CreateMachineFunction;
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
            var body = new 
            {
                Name = "machine-name-test",
                Type = "machine-type-test"
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var machineService = new Mock<IMachineService>();
            //When
            machineService.Setup(ms => ms.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()));

            var res = await new CreateMachine(machineService.Object).Run(req, logger);

            //Then
            machineService.Verify(ms => ms.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>()), Times.Once());
        
        }

        [Fact]
        public async void FucntionShouldReturnOkResult()
        {
            //Given
            var body = new 
            {
                Name = "machine-name-test",
                Type = "machine-type-test"
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var machineService = new Mock<IMachineService>();

            //When
            var res = (OkResult)await new CreateMachine(machineService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task FunctionShouldReturnBadRequestObjectResultIfInvalidDataExceptionIsCaughtAsync()
        {
            //Given
            var body = new 
            {
                Name = "machine-name-test",
                Type = "machine-type-test"
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var machineService = new Mock<IMachineService>();
            
            //When
            machineService.Setup(ms => ms.CreateMachine(It.IsAny<AlarmSystem.Core.Entity.DB.Machine>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new CreateMachine(machineService.Object).Run(req, logger);

            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }
    }
}