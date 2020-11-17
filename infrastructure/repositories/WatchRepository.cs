using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Infrastructure.Repositories

{
    public class WatchRepository : IWatchRepository
    {
        private SystemContext _ctx;

        public WatchRepository(SystemContext ctx)
        {
            _ctx = ctx;
        }

        public void SubscribeToMachine(MachineWatch mw)
        {
            _ctx.MachineWatch.Add(mw);
            _ctx.SaveChanges();
        }

        public void SubscribeToAlarm(AlarmWatch aw)
        {
            
            _ctx.AlarmWatch.Add(aw);
            _ctx.SaveChanges();
		}
		
		public List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId)
        {
            List<MachineWatch> subscriptions = _ctx.MachineWatch.Where(mw => mw.WatchId == watchId).ToList();
            return subscriptions;
        }

        
    }
}