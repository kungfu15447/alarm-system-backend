using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IAlarmService
    {
        Alarm GetAlarmById(int id);
    }
}