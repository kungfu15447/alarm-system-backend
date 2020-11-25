using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        void RemoveMachineSubscriptionFromWatch(MachineWatch mw);
        MachineWatch ReadMachineSubscriptionOfMachineByWatch(string machineId, string watchdId);
        List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        List<MachineWatch> ReadAllMachineSubscriptionsByMachine(string machineId);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByAlarmCode(int alarmCode);
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId);
    }
}