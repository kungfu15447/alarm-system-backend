using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using AlarmSystem.Core.Application;
using System;
using AlarmSystem.Core.Application.Exception;

namespace AlarmSystem.Functions.Subscription.SubscribeToAlarmFunction {

    public class SubscribeToAlarm {
        private IWatchService _watchservice;
        private IAlarmService _alarmservice;

        public SubscribeToAlarm(IWatchService watchService, IAlarmService alarmService) {
            _watchservice = watchService;
            _alarmservice = alarmService;
        }

        [FunctionName("SubscribeToAlarm")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "subscribeToAlarm")] HttpRequest req,
            ILogger log) 
            {
                //TODO Validate body   
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                SubscribeToAlarmModel stam = JsonConvert.DeserializeObject<SubscribeToAlarmModel>(requestBody);
                try {
                    AlarmWatch aw = ParseFunctionModelToDtoModel(stam);

                     _watchservice.SubscribeToAlarm(aw);
                    return new OkResult();
                } catch (InvalidDataException e) 
                {
                    return new BadRequestObjectResult(e.Message);
                } catch (EntityNotFoundException e)
                {
                    return new NotFoundObjectResult(e.Message);
                }
            }

        private AlarmWatch ParseFunctionModelToDtoModel(SubscribeToAlarmModel stam)
        {
            AlarmSystem.Core.Entity.Dto.Alarm alarm = _alarmservice.GetAlarmById(stam.AlarmId);

            AlarmWatch aw = new AlarmWatch()
            {
                Alarm = alarm,
                WatchId = stam.WatchId
            };

            return aw;
        }
    }
}