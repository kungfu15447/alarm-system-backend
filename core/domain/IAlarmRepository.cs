using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;
using core.entity.dto;

namespace AlarmSystem.Core.Domain
{
    public interface IAlarmRepository
    {
        Alarm ReadAlarmByCode(int alarmCode);
        Alarm ReadAlarmById(int id);
		void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
        List<AlarmWithSubscription> ReadAllMachinesWithSubs(string watchId);
	}
}