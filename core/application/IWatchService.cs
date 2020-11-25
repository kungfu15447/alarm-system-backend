using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Application
{
    public interface IWatchService
    {
        void DeleteMachineSubscriptionFromWatch(MachineWatch mw);
        MachineWatch GetMachineSubcriptionOfMachineFromWatch(string machineId, string watchId);
        List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId);
        List<MachineWatch> GetMachineSubscriptionsByMachine(string machineId);
        List<AlarmWatch> GetAlarmSubscriptionsByAlarmCode(int alarmCode);
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
        List<AlarmWatch> GetAlarmSubscriptionsFromWatch(string watchId);
    }
}