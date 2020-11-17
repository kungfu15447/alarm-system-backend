using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Infrastructure;

namespace infrastructure.repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private SystemContext _ctx;
 
        public AlarmRepository(SystemContext ctx) {
            _ctx = ctx;
        }
        public Alarm ReadAlarmById(int id)
        {
            return _ctx.Alarms.FirstOrDefault(a => a.AlarmId == id);
        }
    }
}