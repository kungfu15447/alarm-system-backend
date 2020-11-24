using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
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

        public List<AlarmWatch> ReadAllAlarmSubscriptionsByAlarmCode(int alarmCode)
        {
            List<AlarmWatch> subscriptions = _ctx.AlarmWatch.Include(aw => aw.Alarm).Where(aw => aw.Alarm.Code == alarmCode).ToList();
            return subscriptions;
        }

        public List<MachineWatch> ReadAllMachineSubscriptionsByMachine(string machineId)
        {
            List<MachineWatch> subscriptions = _ctx.MachineWatch.Include(mw => mw.Machine).Where(mw => mw.Machine.MachineId == machineId).ToList();
            return subscriptions;
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
    }
}