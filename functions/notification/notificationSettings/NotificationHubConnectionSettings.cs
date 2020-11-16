using System;
using Microsoft.Azure.NotificationHubs;

namespace AlarmSystem.Functions.Notification.NotificationSettings
{
    public class NotificationHubConnectionSettings : INotificationHubConnectionSettings
    {
        public INotificationHubClient Hub { get; set; }

        public NotificationHubConnectionSettings()
        {
            string accessSignature = Environment.GetEnvironmentVariable("DefaultFullSharedAccessSignature");
            string hubName = Environment.GetEnvironmentVariable("NotificationHubName");

            Hub = new NotificationHubClient(accessSignature, hubName);
        }
    }
}