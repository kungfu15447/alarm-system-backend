using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Exception;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Notification.NotificationSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.Notification
{
    public class SendAlert 
    {
        private IWatchService _watchService;
        private IAlarmService _alarmService;
        private IMachineService _machineService;
        private IAlarmLogService _alarmLogService;
        private readonly INotificationHubClient _hub;
        public SendAlert(IWatchService watchService, IAlarmService alarmService, IMachineService machineService, IAlarmLogService alarmLogService, INotificationHubConnectionSettings hub) 
        {
            _hub = hub.Hub;
            _watchService = watchService;
            _alarmService = alarmService;
            _machineService = machineService;
            _alarmLogService = alarmLogService;
        }
        
        [FunctionName("SendAlert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "notify")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SendAlertModel sam = JsonConvert.DeserializeObject<SendAlertModel>(requestBody);

            List<string> watches = new List<string>();

            AlarmSystem.Core.Entity.DB.AlarmLog alarmLog;

            try 
            {
                watches = GetWatchesToNotify(sam);
                alarmLog = CreateAlarmLog(sam);
            }
            catch(InvalidDataException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch(EntityNotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }

            string accessSignature = Environment.GetEnvironmentVariable("DefaultFullSharedAccessSignature");
            string hubName = Environment.GetEnvironmentVariable("NotificationHubName");

            Microsoft.Azure.NotificationHubs.Notification nof = new FcmNotification(MakeJsonPayload(alarmLog));

            if (watches.Count == 0) {
                return new NoContentResult();
            }

            foreach (string watch in watches) {
                await _hub.SendDirectNotificationAsync(nof, watch);
            }

            return new OkResult();
        }

        private string MakeJsonPayload(AlarmSystem.Core.Entity.DB.AlarmLog al)
        {
            string errorMessage = $"Machine {al.Machine.Name}, {al.Machine.Type} has triggered alarm with code {al.Alarm.Code}";
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

        private AlarmSystem.Core.Entity.DB.AlarmLog CreateAlarmLog(SendAlertModel sam)
        {
            AlarmSystem.Core.Entity.DB.Alarm alarm = _alarmService.GetAlarmByCode(sam.AlarmCode);
            AlarmSystem.Core.Entity.DB.Machine machine = _machineService.GetMachineById(sam.MachineId);
            var date = DateTime.UtcNow;
            long epochOfNow = new DateTimeOffset(date).ToUnixTimeMilliseconds();
            AlarmSystem.Core.Entity.DB.AlarmLog al = new AlarmSystem.Core.Entity.DB.AlarmLog() 
            { 
                Alarm = alarm, Machine = machine, Date = epochOfNow 
            };

            _alarmLogService.CreateAlarmLog(al);

            return al;
        }
    }
}