using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        List<MachineWatch> ReadAllMachineSubscriptionsByMachine(string machineId);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByAlarmCode(int alarmCode);
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId);
    }
}