using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AlarmSystem.Core.Domain;

namespace infrastructure.repositories
{
    public class AlarmLogRepository : IAlarmLogRepository
    {
        private SystemContext _ctx;
 
        public AlarmLogRepository(SystemContext ctx) {
            _ctx = ctx;
        }

        public void AddAlarmLog(AlarmLog alarmLog)
        {
            _ctx.AlarmLogs.Add(alarmLog);
            _ctx.SaveChanges();
        }

        public List<AlarmLog> GetAlarmLog()
        {
            return _ctx.AlarmLogs.Include(al => al.Alarm)
                                 .Include(al => al.Machine).ToList();
        }
    }
}