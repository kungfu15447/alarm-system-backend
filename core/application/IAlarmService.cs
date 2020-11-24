using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IAlarmService
    {
        Alarm GetAlarmByCode(int alarmCode);
        Alarm GetAlarmById(int id);
		void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
	}
}