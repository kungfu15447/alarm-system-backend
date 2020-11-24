

using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.Entity;
using Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions.GetAlarms
{
    public class GetAlarms
    {
        private IAlarmService _alarmService;
        public GetAlarms(IAlarmService alarmService)
       {
           _alarmService = alarmService;
       }

        [FunctionName("GetAllAlarms")]
       public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alarms")] HttpRequest req,
            ILogger log)
        {
            List<Alarm> alarms = _alarmService.GetAllAlarms();

            if (alarms.Count == 0) {
                return new NoContentResult();
            }
            return new OkObjectResult(alarms);
        }
    }
}