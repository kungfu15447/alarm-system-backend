using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Notfification
{
    public class SendAlert 
    {
        public SendAlert() 
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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SendAlertModel sam = JsonConvert.DeserializeObject<SendAlertModel>(requestBody);

            string accessSignature = Environment.GetEnvironmentVariable("DefaultFullSharedAccessSignature");
            string hubName = Environment.GetEnvironmentVariable("NotificationHubName");

            NotificationHubClient hub = new NotificationHubClient(accessSignature, hubName);

            Notification nof = new FcmNotification(MakeJsonPayload(sam));

            //TODO Should be replaced with tokens machine/alarm subscriptions!
            string watchToken = Environment.GetEnvironmentVariable("EmulatorWatchToken");
            await hub.SendDirectNotificationAsync(nof, watchToken);

            return new OkResult();
        }

        private string MakeJsonPayload(SendAlertModel sam)
        {
            string errorMessage = $"The machine {sam.MachineId} has error {sam.AlarmCode}!";
            string jsonPayload = JsonConvert.SerializeObject(new {data = new { message = errorMessage } });
            return jsonPayload;
        }
    }
}