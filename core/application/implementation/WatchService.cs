using AlarmSystem.Core.Entity.Dto;
using core.domain;

namespace core.application.implementation
{
    public class WatchService : IWatchService
    {
        private IWatchRepository _watchRepo;

        public WatchService(IWatchRepository watchRepo) {
            _watchRepo = watchRepo;
        }

        public void SubscribeToMachine(MachineWatch mw) {
            _watchRepo.SubscribeToMachine(mw);  
        }

        public void SubscribeToAlarm(AlarmWatch aw) {
            _watchRepo.SubscribeToWatch(aw);
        }

    }
}
