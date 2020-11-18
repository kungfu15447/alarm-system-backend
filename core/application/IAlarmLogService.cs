using System;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public interface IAlarmLogService
    {
         List<AlarmLog> GetAlarmLog();
    }
}