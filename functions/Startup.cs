using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AlarmSystem.Functions.Startup))]

namespace AlarmSystem.Functions {
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IMachineService, MachineService>();
            builder.Services.AddScoped<IMachineRepository, MachineRepository>();
        }
    }
}