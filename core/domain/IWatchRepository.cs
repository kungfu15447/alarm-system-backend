using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
		List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId);
        void RemoveAlarmSubscriptionFromWatch(AlarmWatch aw);
        AlarmWatch ReadSubscriptionOfAlarmFromWatch(int alarmId, string watchId);
    }
}