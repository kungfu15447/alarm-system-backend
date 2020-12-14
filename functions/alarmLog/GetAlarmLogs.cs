using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Functions.AlarmLog
{
    public class GetAlarmLogs
    {
        private IAlarmLogService _alarmService;
        private IAuthenticationService _authService;
        public GetAlarmLogs(IAlarmLogService alarmService, IAuthenticationService authenticationService) 
        {
            _alarmService = alarmService;
            _authService = authenticationService;
        }

        [FunctionName("GetAlarmLog")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alarmlog")] HttpRequest req,
            ILogger log) 
        {
            Microsoft.Extensions.Primitives.StringValues value; 
            var headers = req.Headers.TryGetValue("Authorization", out value);
            if(headers){
                var bearer = value[0];
                var token = bearer.Split(" ")[1];
                var decryptedToken = _authService.DecryptToken(token);
                if(decryptedToken){
                    List<AlarmSystem.Core.Entity.DB.AlarmLog> alarmLogs = _alarmService.GetAlarmLog();
                    return new OkObjectResult(alarmLogs);
                }
                return new UnauthorizedResult();
            }
            return new UnauthorizedResult();
        }
    }
}