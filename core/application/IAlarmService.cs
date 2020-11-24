using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.Entity;

namespace Core.Application
{
    public interface IAlarmService
    {
        void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
    }
}