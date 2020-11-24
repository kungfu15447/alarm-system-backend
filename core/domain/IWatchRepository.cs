using System.Collections.Generic;
using AlarmSystem.Core.Entity.Entity;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
    }
}