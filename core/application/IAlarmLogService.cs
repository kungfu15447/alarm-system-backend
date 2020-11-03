using System;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace core.application
{
    public interface IAlarmLogService
    {
         List<AlarmLog> GetAlarmLog();
    }
}