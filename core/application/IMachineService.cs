using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Application
{
    public interface IMachineService
    {
        void CreateMachine();
        List<Machine> GetMachines();
        List<MachineWithSubscription> GetAllMachinesWithSubs(string watchId);
        Machine GetMachineById(string id);
    }
}