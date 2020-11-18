using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IWatchRepository
    {
        List<MachineWatch> ReadAllMachineSubscriptionsByWatch(string watchId);
        void RemoveMachineSubscriptionFromWatch(MachineWatch mw);
        MachineWatch ReadMachineSubscriptionOfMachineByWatch(string machineId, string watchdId);
    }
}