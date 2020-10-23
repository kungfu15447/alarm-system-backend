using AlarmSystem.Core.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace AlarmSystem.Infrastructure {
    public class SystemContext : DbContext {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) {}

        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        public DbSet<AlarmWatch> AlarmWatch { get; set; }
        public DbSet<MachineWatch> MachineWatch { get; set; }
    }
}