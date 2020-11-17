using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AlarmService : IAlarmService
    {
        private IAlarmRepository _alarmRepository;

        public AlarmService(IAlarmRepository alarmRepo)
        {
            _alarmRepository = alarmRepo;
        }

        public Alarm GetAlarmById(int id)
        {
            return _alarmRepository.GetAlarmById(id);
        }
    }
}