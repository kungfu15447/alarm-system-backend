using System;
using System.IO;
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
            if(id >= 1) {
                Alarm alarm =  _alarmRepository.ReadAlarmById(id);

                if(alarm != null) {
                    return alarm;
                } else {
                    throw new InvalidDataException($"No alarm was found with id: {id}");
                }
            
            }
            throw new InvalidDataException($"the entered id: {id} must be higher than 0");
            
        }
    }
}