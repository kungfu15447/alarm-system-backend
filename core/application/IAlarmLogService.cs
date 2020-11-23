using System;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IAlarmLogService
    {
         List<AlarmLog> GetAlarmLog();
         void CreateAlarmLog(AlarmLog alarmLog);
    }
}