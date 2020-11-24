using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace functions.alarm
{
    public class GetAlarmById
    {
        private IAlarmService _alarmService;

        public GetAlarmById(IAlarmService alarmService) {
            _alarmService = alarmService;   
        }

        [FunctionName("GetAlarmById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAlarmById/{id}")] HttpRequest req,
            ILogger log, int id ) {
                
                Alarm alarm =  _alarmService.GetAlarmById(id);
                return new OkObjectResult(alarm);
        }
    }
}