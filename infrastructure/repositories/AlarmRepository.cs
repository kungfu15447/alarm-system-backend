using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Infrastructure;
using core.domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private SystemContext _ctx;
 
        public AlarmRepository(SystemContext ctx) {
            _ctx = ctx;
        }

        public List<AlarmLog> GetAlarmLog()
        {
            return _ctx.AlarmLogs.Include(al => al.Alarm)
                                 .Include(al => al.Machine).ToList();
                  
        }
    }
}