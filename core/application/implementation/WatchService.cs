using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public class WatchService : IWatchService
    {
        private IWatchRepository _watchRepo;
        public WatchService(IWatchRepository watchRepo) {
            _watchRepo = watchRepo;
        }

        public void DeleteMachineSubscriptionFromWatch(MachineWatch mw)
        {
            _watchRepo.RemoveMachineSubscriptionFromWatch(mw);
        }

        //TODO Validate machine and watch id
        //TODO Check to see if the returned MachineWatch is null
        public MachineWatch GetMachineSubcriptionOfMachineFromWatch(string machineId, string watchId)
        {
            return _watchRepo.ReadMachineSubscriptionOfMachineByWatch(machineId, watchId);
        }

        public List<MachineWatch> GetMachineSubscriptionsFromWatch(string watchId)
        {
            if (string.IsNullOrEmpty(watchId)) {
                throw new InvalidDataException("Watch id cannot be empty or non existent! Please include watch id");
            }
            return _watchRepo.ReadAllMachineSubscriptionsByWatch(watchId);
        }
    }
}