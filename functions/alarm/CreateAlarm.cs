using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.CreateAlarm
{
    public class CreateAlarm
    {
       private IAlarmService _alarmService;
       public CreateAlarm(IAlarmService alarmService)
       {
           _alarmService = alarmService;
       }

       [FunctionName("CreateAlarm")]
       public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "alarms")] HttpRequest req,
            ILogger log)
            {
                string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
                var alarm = JsonConvert.DeserializeObject<Alarm>(reqBody);
                _alarmService.CreateAlarm(alarm);
                return new OkResult();
            }
    }
}