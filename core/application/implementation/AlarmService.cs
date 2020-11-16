using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AlarmService : IAlarmService
    {
        private IAlarmRepository _alarmRepo;
        public AlarmService(IAlarmRepository alarmRepo)
        {
            _alarmRepo = alarmRepo;
        }
        public Alarm GetAlarmByCode(int alarmCode)
        {
            return _alarmRepo.ReadAlarmByCode(alarmCode);
        }
    }
}