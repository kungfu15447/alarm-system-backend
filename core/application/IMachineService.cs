using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IMachineService
    {
        void CreateMachine();
        List<Machine> GetMachines();
    }
}