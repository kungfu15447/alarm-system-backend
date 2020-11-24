using System.Collections.Generic;
using AlarmSystem.Core.Entity.Entity;

namespace AlarmSystem.Core.Application
{
    public interface IWatchService
    {
        List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId);
    }
}