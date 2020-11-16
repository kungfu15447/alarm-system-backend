using Microsoft.Azure.NotificationHubs;

namespace AlarmSystem.Functions.Notification.NotificationSettings
{
    public interface INotificationHubConnectionSettings
    {
        NotificationHubClient Hub { get; set; }
    }
}