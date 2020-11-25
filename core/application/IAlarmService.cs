using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;
using core.entity.dto;

namespace AlarmSystem.Core.Application
{
    public interface IAlarmService
    {
        Alarm GetAlarmByCode(int alarmCode);
        Alarm GetAlarmById(int id);
		void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
        List<AlarmWithSubscription> GetAllAlarmsWithSubs(string watchId);
	}
}