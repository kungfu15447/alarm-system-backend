using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using core.entity.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace functions.alarm
{
    public class GetAlarmsWithSubs
    {
        private IAlarmService _alarmService;
        public GetAlarmsWithSubs(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        [FunctionName("GetAlarmsWithSubs")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alarms/{watchId}")] HttpRequest req,
            ILogger log, string watchId)
        {
            List<AlarmWithSubscription> machinesWithSubs = _alarmService.GetAllAlarmsWithSubs(watchId);

            if (machinesWithSubs.Count == 0) {
                return new NoContentResult();
            }

            return new OkObjectResult(machinesWithSubs);
        }
    }
}