using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;

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

        public List<MachineWithSubscription> ReadAllMachinesWithSubs(string watchId){

            var listOfSubs = _ctx.MachineWatch.Where(mw => watchId == mw.WatchId);
            var listOfMachines = _ctx.Machines.Select(m => new MachineWithSubscription{
                MachineId = m.MachineId,
                IsSubscribed = listOfSubs.FirstOrDefault(mw => mw.Machine.MachineId ==  m.MachineId && watchId == mw.WatchId).WatchId  == watchId
            });


            return listOfMachines.ToList();
        }
        public Machine ReadMachineById(string id)
        {
            return _ctx.Machines.Where(m => m.MachineId == id).FirstOrDefault();
        }
    }


        
}
