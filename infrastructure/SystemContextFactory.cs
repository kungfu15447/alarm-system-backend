using System;
using AlarmSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlarmSystem.Functions {
    public class SystemContextFactory : IDesignTimeDbContextFactory<SystemContext>
    {
        public SystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SystemContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new SystemContext(optionsBuilder.Options);
        }
    }
}