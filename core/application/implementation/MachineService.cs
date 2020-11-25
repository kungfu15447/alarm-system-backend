using System;
using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;

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

        public Machine GetMachineById(string id)
        {
            Machine machine =  _machineRepo.ReadMachineById(id);

            if(machine != null) {
                return machine;
            } else {
                throw new EntityNotFoundException($"No machine was found with id: {id}");
            }
        }

        public List<Machine> GetMachines()
        {
            return _machineRepo.ReadAllMachines();
        }

        public List<MachineWithSubscription> GetAllMachinesWithSubs(string watchId)
        {
            if (string.IsNullOrEmpty(watchId)) {
                throw new InvalidDataException("Watch id cannot be empty or non existent! Please include watch id");
            }
            return _machineRepo.ReadAllMachinesWithSubs(watchId);
        }
    }
}