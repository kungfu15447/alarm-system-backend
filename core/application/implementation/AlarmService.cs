using System;
using System.IO;
using AlarmSystem.Core.Domain;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Application.Exception;

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
          
        public Alarm GetAlarmById(int id)
        {
            if(id >= 1) {
                Alarm alarm =  _alarmRepo.ReadAlarmById(id);

                if(alarm != null) {
                    return alarm;
                } else {
                    throw new EntityNotFoundException($"No alarm was found with id: {id}");
                }
            }
            throw new InvalidDataException($"the entered id: {id} must be higher than 0");
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