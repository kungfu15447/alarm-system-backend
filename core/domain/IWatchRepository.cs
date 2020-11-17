using AlarmSystem.Core.Entity.Dto;

namespace core.domain
{
    public interface IWatchRepository
    {
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
		List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
    }
}