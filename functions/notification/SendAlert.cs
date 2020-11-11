using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
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
        private IWatchService _watchService;
        public SendAlert(IWatchService watchService) 
        {
            _watchService = watchService;
        }

        //TODO Create alarm log
        //TODO Test to see if function works
        //TODO Optimize way to find all watches that needs a notification send
        [FunctionName("SendAlert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "notify")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SendAlertModel sam = JsonConvert.DeserializeObject<SendAlertModel>(requestBody);

            List<string> watches = GetWatchesToNotify(sam);

            string accessSignature = Environment.GetEnvironmentVariable("DefaultFullSharedAccessSignature");
            string hubName = Environment.GetEnvironmentVariable("NotificationHubName");

            NotificationHubClient hub = new NotificationHubClient(accessSignature, hubName);

            Notification nof = new FcmNotification(MakeJsonPayload(sam));

            if (watches.Count == 0) {
                return new NoContentResult();
            }

            foreach (string watch in watches) {
                await hub.SendDirectNotificationAsync(nof, watch);
            }

            return new OkResult();
        }

        private string MakeJsonPayload(SendAlertModel sam)
        {
            string errorMessage = $"The machine {sam.MachineId} has error {sam.AlarmCode}!";
            string jsonPayload = JsonConvert.SerializeObject(new {data = new { message = errorMessage } });
            return jsonPayload;
        }

        private List<string> GetWatchesToNotify(SendAlertModel sam)
        {
            List<string> watches = new List<string>();
            List<MachineWatch> machineSubscriptions = _watchService.GetMachineSubscriptionsByMachine(sam.MachineId);
            List<AlarmWatch> alarmSubscriptions = _watchService.GetAlarmSubscriptionsByAlarmCode(sam.AlarmCode);

            foreach(MachineWatch mw in machineSubscriptions)
            {
                watches.Add(mw.WatchId);
            }

            //Since a watch can both be subscribed to a machine and an alarm. We don't want to send the same notification twice to one watch
            //This should probably be optimised so we can combine the two subscriptions list without duplicates
            foreach(AlarmWatch aw in alarmSubscriptions)
            {
                bool alreadyIn = watches.Contains(aw.WatchId);

                if (!alreadyIn) 
                {
                    watches.Add(aw.WatchId);
                }
            }
            return watches;
        }
    }
}