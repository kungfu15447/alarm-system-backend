using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain 
{
    public interface IAlarmRepository
    {
        void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();

    }
}