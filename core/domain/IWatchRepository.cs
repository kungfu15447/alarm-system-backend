using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId);
    }
}