using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using core.domain;

namespace core.application.implementation
{
    public class AlarmService : IAlarmService
    {
        private IAlarmRepository _alarmRepository;

        public AlarmService(IAlarmRepository alarmRepository) {
            _alarmRepository = alarmRepository;
        }

        public List<AlarmLog> GetAlarmLog()
        {
            return _alarmRepository.GetAlarmLog();
        }
    }
}