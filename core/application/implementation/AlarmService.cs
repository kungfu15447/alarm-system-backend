using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace Core.Application.Implementation
{
    public class AlarmService : IAlarmService
    {
        private IAlarmRepository _alarmRepo;
        public AlarmService(IAlarmRepository alarmRepo)
        {
            _alarmRepo = alarmRepo;
        }
        public void CreateAlarm(Alarm alarm)
        {
            _alarmRepo.CreateAlarm(alarm);
        }
        public List<Alarm> GetAllAlarms()
        {
            return _alarmRepo.GetAllAlarms();
        }
    }
}