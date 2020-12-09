using System;
using System.Collections.Generic;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Functions.Notification.NotificationSettings;
using AlarmSystem.Infrastructure;
using AlarmSystem.Infrastructure.Helpers;
using AlarmSystem.Infrastructure.Repositories;
using infrastructure.repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AlarmSystem.Functions.Startup))]

namespace AlarmSystem.Functions {
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            string sqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<SystemContext>(
                options => options.UseSqlServer(sqlConnection)
            );
            
            builder.Services.AddScoped<INotificationHubConnectionSettings, NotificationHubConnectionSettings>();

            builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
            builder.Services.AddScoped<IAlarmService, AlarmService>();

            builder.Services.AddScoped<IMachineService, MachineService>();
            builder.Services.AddScoped<IMachineRepository, MachineRepository>();
            
            builder.Services.AddScoped<IAlarmLogService, AlarmLogService>();
            builder.Services.AddScoped<IAlarmLogRepository, AlarmLogRepository>();

            builder.Services.AddScoped<IWatchService, WatchService>();
            builder.Services.AddScoped<IWatchRepository, WatchRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<IAuthenticationHelper, AuthenticationHelper>(b => {
                string secretKey = Environment.GetEnvironmentVariable("SecretToken");
                return new AuthenticationHelper(System.Text.Encoding.UTF32.GetBytes(secretKey));
            });
        }

    }
}