using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Subscription.SubscribeToMachineFunction;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class SubscribeToMachineTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallWatchServiceOnce() {
            //Given
            var submodel = new SubscribeToMachineModel(){
                MachineId = "1",
                WatchId = "1"
            };
            var req = new HttpRequestBuilder().Body(submodel).Build();
            
            var watchService = new Mock<IWatchService>();
            var machineService = new Mock<IMachineService>();
            
            MachineWatch mw = new MachineWatch{
                Machine = new Core.Entity.DB.Machine {
                    MachineId = "1"
                },
                WatchId = "1"
            };

            //When
            watchService.Setup(ms => ms.SubscribeToMachine(mw));

            var res = await new SubscribeToMachine(watchService.Object, machineService.Object).Run(req, logger);

            //Then
            watchService.Verify(ms => ms.SubscribeToMachine(It.IsAny<MachineWatch>()), Times.Once());
            
        }

        [Fact]
        public async void FunctionShouldReturnOkResult()
        {
            //Given
            var body = new SubscribeToMachineModel(){
                MachineId = "machine-id-1",
                WatchId = "watch-id-1"
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();
            var machineService = new Mock<IMachineService>();

            var machine = new AlarmSystem.Core.Entity.DB.Machine() {
                MachineId = "machine-id-1"
            };

            watchService.Setup(ws => ws.SubscribeToMachine(It.IsAny<MachineWatch>()));
            machineService.Setup(ms => ms.GetMachineById(It.IsAny<string>())).Returns(machine);

            //When
            var res = (OkResult)await new SubscribeToMachine(watchService.Object, machineService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }
    }
}