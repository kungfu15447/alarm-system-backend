using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using System;

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
            Alarm alarm = _alarmRepo.ReadAlarmByCode(alarmCode);

            if (alarm != null) {
                return alarm; 
            } else {
                throw new EntityNotFoundException("Could not find entity in database!");
            }
        }
    }
}