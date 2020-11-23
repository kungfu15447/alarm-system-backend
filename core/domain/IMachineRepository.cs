using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using core.entity.dto;

namespace AlarmSystem.Core.Domain
{
    public interface IMachineRepository
    {
        void CreateMachine(Machine machine);
        List<Machine> ReadAllMachines();

        List<Dto_Machine> ReadAllMachinesWithSubs(string WatchId);
    }
}