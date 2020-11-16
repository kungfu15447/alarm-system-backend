using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using System.Linq;

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
    }
}