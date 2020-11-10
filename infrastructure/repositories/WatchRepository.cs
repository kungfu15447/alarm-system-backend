using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Infrastructure.Repositories
{
    public class WatchRepository : IWatchRepository
    {
        public List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId)
        {
            throw new System.NotImplementedException();
        }
    }
}