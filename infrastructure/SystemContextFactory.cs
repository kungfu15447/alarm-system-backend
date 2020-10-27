using System;
using System.IO;
using AlarmSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlarmSystem.Functions {
    public class SystemContextFactory : IDesignTimeDbContextFactory<SystemContext>
    {
        public SystemContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("infrastructure.config.json", optional: false, reloadOnChange: true);
            
            var config = builder.Build();

            var connectionString = config.GetConnectionString("SQL");
            var optionsBuilder = new DbContextOptionsBuilder<SystemContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SystemContext(optionsBuilder.Options);
        }
    }
}