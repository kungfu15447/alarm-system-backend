using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Alarm
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
            List<AlarmSystem.Core.Entity.DB.Alarm> alarms = _alarmService.GetAllAlarms();

            if (alarms.Count == 0) {
                return new NoContentResult();
            }
            return new OkObjectResult(alarms);
        }
    }
}