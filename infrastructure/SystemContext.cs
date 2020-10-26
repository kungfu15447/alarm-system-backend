using AlarmSystem.Core.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace AlarmSystem.Infrastructure {
    public class SystemContext : DbContext {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Alarm>()
                .Property(a => a.AlarmId)
                .HasColumnName("Id");

            modelBuilder.Entity<Machine>()
                .Property(m => m.MachineId)
                .HasColumnName("Id");
            
            modelBuilder.Entity<AlarmLog>()
                .HasKey(al => new { al.AlarmId, al.MachineId, al.Date });

            modelBuilder.Entity<AlarmWatch>()
                .HasKey(aw => new { aw.AlarmId, aw.WatchId });

            modelBuilder.Entity<MachineWatch>()
                .HasKey(mw => new { mw.MachineId, mw.WatchId });
        }

        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        public DbSet<AlarmWatch> AlarmWatch { get; set; }
        public DbSet<MachineWatch> MachineWatch { get; set; }
    }
}