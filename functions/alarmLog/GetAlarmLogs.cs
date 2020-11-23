using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.AlarmLog
{
    public class GetAlarmLogs
    {
        private IAlarmLogService _alarmService;
        public GetAlarmLogs(IAlarmLogService alarmService) 
        {
            _alarmService = alarmService;
        }

        [FunctionName("GetAlarmLog")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alarmlog")] HttpRequest req,
            ILogger log) 
        {
            List<AlarmSystem.Core.Entity.Dto.AlarmLog> alarmLogs =  _alarmService.GetAlarmLog();
            return new OkObjectResult(alarmLogs);
        }
    }
}