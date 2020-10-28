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
    }
}