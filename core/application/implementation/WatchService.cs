using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Application.Exception;

namespace AlarmSystem.Core.Application.Implementation
{
    public class WatchService : IWatchService
    {
        private IWatchRepository _watchRepo;
		
        public WatchService(IWatchRepository watchRepo) {
            _watchRepo = watchRepo;
        }

        public List<AlarmWatch> GetAlarmSubscriptionsByAlarmCode(int alarmCode)
        {
            return _watchRepo.ReadAllAlarmSubscriptionsByAlarmCode(alarmCode);
        }

        public List<MachineWatch> GetMachineSubscriptionsByMachine(string machineId)
        {
            if (string.IsNullOrEmpty(machineId)) {
                throw new InvalidDataException("Machine Id cannot be empty or non existent! Please include machine id");
            }
            return _watchRepo.ReadAllMachineSubscriptionsByMachine(machineId);
        }

        public void SubscribeToMachine(MachineWatch mw) 
        {
            _watchRepo.SubscribeToMachine(mw);  
        }

        public void SubscribeToAlarm(AlarmWatch aw) 
        {
            _watchRepo.SubscribeToAlarm(aw);
		}
        
        public List<AlarmWatch> GetAlarmSubscriptionsFromWatch(string watchId)
        {
            if (string.IsNullOrEmpty(watchId)) {
                throw new InvalidDataException("Watch id cannot be empty or non existent! Please include watch id");
            }
            return _watchRepo.ReadAllAlarmSubscriptionsByWatch(watchId);
        }

        public List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId)
        {
            if (string.IsNullOrEmpty(watchId)) {
                throw new InvalidDataException("Watch id cannot be empty or non existent! Please include watch id");
            }
            return _watchRepo.ReadAllMachineSubscriptionsByWatch(watchId);
        }

        public void DeleteAlarmSubscriptionFromWatch(AlarmWatch aw)
        {
            _watchRepo.RemoveAlarmSubscriptionFromWatch(aw);
        }

        public AlarmWatch GetSubscriptionOfAlarmFromWatch(int alarmId, string watchId)
        {
            if (string.IsNullOrEmpty(watchId)) 
            {
                throw new InvalidDataException("Watch id cannot be empty or non existent! Please include watch id");
            }
            else 
            {
                AlarmWatch aw = _watchRepo.ReadSubscriptionOfAlarmFromWatch(alarmId, watchId);

                if (aw != null) 
                {
                    return aw;
                }
                else
                {
                    throw new EntityNotFoundException("Entity was not found in database");
                }
            }
        }
    }
}
