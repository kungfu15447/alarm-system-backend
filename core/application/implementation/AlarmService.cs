using System;
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
            Alarm alarm =  _alarmRepository.ReadAlarmById(id);

            if(alarm != null) {
                return alarm;
            } else {
                throw new Exception($"No alarm was found with id: {id}");
            }
        }
    }
}