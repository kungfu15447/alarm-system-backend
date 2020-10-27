using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Entity.Dto;
using core.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace functions.alarm
{
    public class GetAlarmLog
    {
        private IAlarmService _alarmService;
        public GetAlarmLog(IAlarmService alarmService) {
            _alarmService = alarmService;
        }

        [FunctionName("GetAlarmLog")]
        public async Task<List<AlarmLog>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log) {

                List<AlarmLog> alarmLog =  _alarmService.GetAlarmLog();
                return alarmLog;
        }
    }
}