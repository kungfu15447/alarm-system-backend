using System.Collections.Generic;
using AlarmSystem.Core.Entity.Entity;
using AlarmSystem.Core.Domain;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AlarmLogService : IAlarmLogService
    {
        private IAlarmLogRepository _alarmRepository;

        public AlarmLogService(IAlarmLogRepository alarmRepository) {
            _alarmRepository = alarmRepository;
        }

        public List<AlarmLog> GetAlarmLog()
        {
            return _alarmRepository.GetAlarmLog();
        }
    }
}