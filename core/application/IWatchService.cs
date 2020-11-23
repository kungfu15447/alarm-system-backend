using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IWatchService
    {
        void SubscribeToMachine(MachineWatch mw);
        void SubscribeToAlarm(AlarmWatch aw);
		List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId);
        List<AlarmWatch> GetAlarmSubscriptionsFromWatch(string watchId);
        void DeleteAlarmSubscriptionFromWatch(AlarmWatch aw);
    }
}