using AlarmSystem.Core.Entity.Dto;

namespace core.domain
{
    public interface IWatchRepository
    {
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToWatch(AlarmWatch aw);
        
    }
}