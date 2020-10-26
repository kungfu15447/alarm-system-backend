using AlarmSystem.Core.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace AlarmSystem.Infrastructure {
    public class SystemContext : DbContext {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Alarm>()
                .HasKey(a => a.AlarmId)
                .HasName("Id");

            modelBuilder.Entity<Machine>()
                .HasKey(m => m.MachineId)
                .HasName("Id");
            
            modelBuilder.Entity<AlarmLog>()
                .HasKey(al => new { al.Alarm, al.Machine, al.Date })
                .HasName("Id");

            modelBuilder.Entity<AlarmWatch>()
                .HasKey(aw => new { aw.Alarm, aw.WatchId })
                .HasName("Id");

            modelBuilder.Entity<MachineWatch>()
                .HasKey(mw => new { mw.Machine, mw.WatchId })
                .HasName("Id");
            
            modelBuilder.Entity<AlarmLog>()
                .HasOne(al => al.Alarm)
                .WithOne();
            
            modelBuilder.Entity<AlarmLog>()
                .HasOne(al => al.Machine)
                .WithOne();
            
            modelBuilder.Entity<AlarmWatch>()
                .HasOne(aw => aw.Alarm)
                .WithOne();

            modelBuilder.Entity<MachineWatch>()
                .HasOne(mw => mw.Machine)
                .WithOne();
        }

        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        public DbSet<AlarmWatch> AlarmWatch { get; set; }
        public DbSet<MachineWatch> MachineWatch { get; set; }
    }
}