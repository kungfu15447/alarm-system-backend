using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private SystemContext _ctx;
        public MachineRepository(SystemContext ctx)
        {
            _ctx = ctx;
        }

        public void CreateMachine(Machine machine)
        {
            _ctx.Machines.Add(machine);
            _ctx.SaveChanges();
        }

        public List<Machine> ReadAllMachines()
        {
            return _ctx.Machines.ToList();
        }

        public Machine ReadMachineById(string id)
        {
            return _ctx.Machines.Where(m => m.MachineId == id).FirstOrDefault();
        }
    }
}