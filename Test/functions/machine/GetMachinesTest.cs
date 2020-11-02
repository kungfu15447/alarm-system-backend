using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Functions.Machine;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Machine
{
    public class GetMachinesTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public void FunctionShouldCallGetMachinesFromServiceOnce()
        {
            //Given
            var machineService = new Mock<IMachineService>();
            var req = new HttpRequestBuilder().Build();

            //When
            machineService.Setup(ms => ms.GetMachines()).Returns(It.IsAny<List<AlarmSystem.Core.Entity.Dto.Machine>>());

            var res = new GetMachines(machineService.Object).Run(req, logger);

            //Then
            machineService.Verify(ms => ms.GetMachines(), Times.Once);
        }

        [Fact]
        public async Task FunctionShouldReturnOkObjectResultIfListIsNotEmptyAsync()
        {
            //Given
            var machineService = new Mock<IMachineService>();
            var req = new HttpRequestBuilder().Build();

            List<Core.Entity.Dto.Machine> machines = new List<Core.Entity.Dto.Machine>(){
                new Core.Entity.Dto.Machine() { MachineId = "this-is-a-test" }
            };

            //When
            machineService.Setup(ms => ms.GetMachines()).Returns(machines);

            var res = (OkObjectResult) await new GetMachines(machineService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task FunctionShouldReturnNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var machineService = new Mock<IMachineService>();
            var req = new HttpRequestBuilder().Build();

            List<Core.Entity.Dto.Machine> machines = new List<Core.Entity.Dto.Machine>();
            //When
            machineService.Setup(ms => ms.GetMachines()).Returns(machines);
            
            var res = (NoContentResult) await new GetMachines(machineService.Object).Run(req, logger);
        
            //Then
            Assert.IsType<NoContentResult>(res);
        }
    }
}