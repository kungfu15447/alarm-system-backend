using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.EntityFrameworkCore;

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
		
        public List<AlarmWatch> ReadAllAlarmSubscriptionsByWatch(string watchId)
        {
            List<AlarmWatch> subscriptions = _ctx.AlarmWatch.Include(aw => aw.Alarm).Where(aw => aw.WatchId == watchId).ToList();
            return subscriptions;
        }

        public List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId)
        {
            List<MachineWatch> subscriptions = _ctx.MachineWatch.Where(mw => mw.WatchId == watchId).ToList();
            return subscriptions;
        }

        public MachineWatch ReadMachineSubscriptionOfMachineByWatch(string machineId, string watchdId)
        {
            MachineWatch mw = _ctx.MachineWatch
                                .Include(mw => mw.Machine)
                                .Where(mw => mw.Machine.MachineId == machineId && mw.WatchId == watchdId)
                                .FirstOrDefault();
            return mw;
        }

        public void RemoveMachineSubscriptionFromWatch(MachineWatch mw)
        {
            _ctx.MachineWatch.Remove(mw);
        }
    }
}