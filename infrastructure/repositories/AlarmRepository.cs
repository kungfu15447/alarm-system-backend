using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Infrastructure;

namespace infrastructure.repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private SystemContext _ctx;
        public AlarmRepository(SystemContext ctx) => _ctx = ctx;

        public void CreateAlarm(Alarm alarm)
        {
            _ctx.Alarms.Add(alarm);
            _ctx.SaveChanges();
        }

        public List<Alarm> GetAllAlarms()
        {
            return _ctx.Alarms.ToList();
        }
    }
}