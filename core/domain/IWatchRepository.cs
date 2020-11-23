using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        void RemoveMachineSubscriptionFromWatch(MachineWatch mw);
        MachineWatch ReadMachineSubscriptionOfMachineByWatch(string machineId, string watchdId);
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
		List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId);
    }
}