using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using core.entity.dto;

namespace AlarmSystem.Core.Application
{
    public interface IMachineService
    {
        void CreateMachine();
        List<Machine> GetMachines();
        List<Dto_Machine> GetAllMachinesWithSubs(string watchId);
    }
}