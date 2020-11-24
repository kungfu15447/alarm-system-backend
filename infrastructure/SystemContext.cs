using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;
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
                .Property<int>("AlarmId");

            modelBuilder.Entity<AlarmLog>()
                .Property<string>("MachineId");
            
            modelBuilder.Entity<AlarmLog>()
                .HasKey(new string [] { "AlarmId", "MachineId", "Date" });
            
            modelBuilder.Entity<AlarmWatch>()
                .Property<int>("AlarmId");

            modelBuilder.Entity<AlarmWatch>()
                .HasKey(new string [] { "AlarmId", "WatchId" });

            modelBuilder.Entity<MachineWatch>()
                .Property<string>("MachineId");

            modelBuilder.Entity<MachineWatch>()
                .HasKey(new string [] { "MachineId", "WatchId" });
        }

        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        public DbSet<AlarmWatch> AlarmWatch { get; set; }
        public DbSet<MachineWatch> MachineWatch { get; set; }
    }
}