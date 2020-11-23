using System;
using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using core.entity.dto;

namespace AlarmSystem.Core.Application.Implementation
{
    public class MachineService : IMachineService
    {
        private IMachineRepository _machineRepo;
        public MachineService(IMachineRepository machineRepo)
        {
            _machineRepo = machineRepo;
        }
        public void CreateMachine()
        {
            Guid guid = Guid.NewGuid();
            Machine machine = new Machine { MachineId = guid.ToString() };
            _machineRepo.CreateMachine(machine);
        }

        public List<Machine> GetMachines()
        {
            return _machineRepo.ReadAllMachines();
        }

        public List<Dto_Machine> GetAllMachinesWithSubs(string watchId)
        {
            return _machineRepo.ReadAllMachinesWithSubs(watchId);
        }
    }
}