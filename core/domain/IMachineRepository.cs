using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Domain
{
    public interface IMachineRepository
    {
        void CreateMachine(Machine machine);
        List<Machine> ReadAllMachines();
    }
}