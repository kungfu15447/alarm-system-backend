using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public class WatchService : IWatchService
    {
        public WatchService() {

        }
        
        public List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId)
        {
            throw new System.NotImplementedException();
        }
    }
}