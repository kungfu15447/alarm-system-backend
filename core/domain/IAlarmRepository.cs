using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IAlarmRepository
    {
        Alarm ReadAlarmByCode(int alarmCode);
        Alarm ReadAlarmById(int id);
		void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
        List<AlarmWithSubscription> ReadAllAlarmsWithSubs(string watchId);
	}
}