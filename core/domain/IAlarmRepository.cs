using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Domain
{
    public interface IAlarmRepository
    {
        Alarm ReadAlarmByCode(int alarmCode);
        Alarm ReadAlarmById(int id);
		void CreateAlarm(Alarm alarm);
        List<Alarm> GetAllAlarms();
	}
}