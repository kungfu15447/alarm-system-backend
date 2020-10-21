using AlarmSystem.Core.Application;
using AlarmSystem.Core.Domain;

namespace AlarmSystem.Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private IMachineService machine;
    }
}