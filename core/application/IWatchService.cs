using AlarmSystem.Core.Entity.Dto;

namespace core.application
{
    public interface IWatchService
    {
        void SubscribeToMachine(MachineWatch mw);

        void SubscribeToAlarm(AlarmWatch aw);
    }
}