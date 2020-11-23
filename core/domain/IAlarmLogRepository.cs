using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IAlarmLogRepository
    {
        List<AlarmLog> GetAlarmLog();
        void AddAlarmLog(AlarmLog alarmLog);
    }
}