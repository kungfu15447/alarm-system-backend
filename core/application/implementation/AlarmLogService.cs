using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AlarmLogService : IAlarmLogService
    {
        private IAlarmLogRepository _alarmRepository;

        public AlarmLogService(IAlarmLogRepository alarmRepository) {
            _alarmRepository = alarmRepository;
        }

        public void CreateAlarmLog(AlarmLog alarmLog)
        {
            _alarmRepository.AddAlarmLog(alarmLog);
        }

        public List<AlarmLog> GetAlarmLog()
        {
            return _alarmRepository.GetAlarmLog();
        }
    }
}