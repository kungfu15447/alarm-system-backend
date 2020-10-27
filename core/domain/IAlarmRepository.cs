using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace core.domain
{
    public interface IAlarmRepository
    {
        List<AlarmLog> GetAlarmLog();
    }
}