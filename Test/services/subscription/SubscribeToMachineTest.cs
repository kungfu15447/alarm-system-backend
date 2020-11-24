using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.Subscription
{
    public class SubscribeToMachineTest
    {
        [Fact]
        public void TestRepoShouldBeCalledOnceIfMachineWatchIsNotEmptyOrNull()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            
            MachineWatch mw = new MachineWatch{
                Machine = new Core.Entity.DB.Machine {
                    MachineId = "1"
                },
                WatchId = "1"
            };

            //When
            mockRepo.Setup(mr => mr.SubscribeToMachine(It.IsAny<MachineWatch>()));
            
        
            service.SubscribeToMachine(mw);
        
            //Then
            mockRepo.Verify(mr => mr.SubscribeToMachine(It.IsAny<MachineWatch>()), Times.Once);
        }

        [Fact]
        public void TestRepoShouldNotBeCalledIfMachineWatchIsNullOrEmpty()
        {
            //Given
            var mockRepo = new Mock<IWatchRepository>();
            var service = new WatchService(mockRepo.Object);
            //When
            mockRepo.Setup(mr => mr.SubscribeToMachine(It.IsAny<MachineWatch>()));

            //Then
            mockRepo.Verify(mr => mr.SubscribeToMachine(It.IsAny<MachineWatch>()), Times.Never);
        }
    }
}