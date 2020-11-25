using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Domain
{
    public interface IMachineRepository
    {
        void CreateMachine(Machine machine);
        List<Machine> ReadAllMachines();
        List<MachineWithSubscription> ReadAllMachinesWithSubs(string WatchId);
        Machine ReadMachineById(string id);

    }
}