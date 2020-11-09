using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Infrastructure;
using core.domain;

namespace infrastructure.repositories
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

        public void SubscribeToWatch(AlarmWatch aw)
        {
            _ctx.AlarmWatch.Add(aw);
            _ctx.SaveChanges();
        }
    }
}