using Microsoft.Azure.NotificationHubs;

namespace AlarmSystem.Functions.Notification.NotificationSettings
{
    public interface INotificationHubConnectionSettings
    {
        INotificationHubClient Hub { get; set; }
    }
}