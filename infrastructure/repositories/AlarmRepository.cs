using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Infrastructure;
using core.entity.dto;

namespace AlarmSystem.Infrastructure.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private SystemContext _ctx;
        public AlarmRepository(SystemContext ctx)
        {
            _ctx = ctx;
        }
        
        public Alarm ReadAlarmByCode(int alarmCode)
        {
            return _ctx.Alarms.Where(a => a.Code == alarmCode).FirstOrDefault();
        }

        public Alarm ReadAlarmById(int id)
        {
            return _ctx.Alarms.FirstOrDefault(a => a.AlarmId == id);
		}

        public void CreateAlarm(Alarm alarm)
        {
            _ctx.Alarms.Add(alarm);
            _ctx.SaveChanges();
        }

        public List<Alarm> GetAllAlarms()
        {
            return _ctx.Alarms.ToList();
        }
        public List<AlarmWithSubscription> ReadAllMachinesWithSubs(string watchId)
        {            
            return _ctx.Alarms.Select(a => new AlarmWithSubscription{
                AlarmId = a.AlarmId,
                Code = a.Code,
                Description = a.Description,
                IsSubscribed = _ctx.AlarmWatch.Any(aw => aw.Alarm.AlarmId == a.AlarmId && aw.WatchId == watchId)
            }).ToList();
        }
    }
}