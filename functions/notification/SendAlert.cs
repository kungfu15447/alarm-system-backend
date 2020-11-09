using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Notfification
{
    public class SendAlart 
    {
        public SendAlart() 
        {

        }

        //TODO Create alarm log
        //TODO Get alarm subscriptions
        //TODO Get machine subscriptions
        //TODO Send notifications to only subscribed individuals
        //TODO Return different results depending on if it send any notifications or not
        [FunctionName("SendAlert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "notify")] HttpRequest req,
            ILogger log)
        {
            string accessSignature = Environment.GetEnvironmentVariable("DefaultFullSharedAccessSignature");
            string hubName = Environment.GetEnvironmentVariable("NotificationHubName");

            NotificationHubClient hub = new NotificationHubClient(accessSignature, hubName);

            Notification nof = new FcmNotification("{\"data\":{\"message\":\"This is for my friend\"}}");


            //TODO Should be replaced with tokens machine/alarm subscriptions!
            string watchToken = Environment.GetEnvironmentVariable("EmulatorWatchToken");
            await hub.SendDirectNotificationAsync(nof,watchToken);

            return new OkResult();
        }
    }
}