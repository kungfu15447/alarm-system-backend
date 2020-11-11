using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IWatchService
    {
        List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId);
        List<MachineWatch> GetMachineSubscriptionsByMachine(string machineId);
        List<AlarmWatch> GetAlarmSubscriptionsByAlarmCode(int alarmCode);
    }
}