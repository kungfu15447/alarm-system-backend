using System.Collections.Generic;
using AlarmSystem.Core.Entity.Entity;

namespace AlarmSystem.Core.Domain 
{
    public interface IAlarmRepository
    {
        void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();

    }
}