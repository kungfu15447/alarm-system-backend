using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IAlarmRepository
    {
        Alarm ReadAlarmByCode(int alarmCode);
    }
}