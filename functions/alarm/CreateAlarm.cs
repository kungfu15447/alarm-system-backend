namespace functions.alarm
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
                _alarmService.CreateAlarm(JsonConvert.DeserializeObject(reqBody));
                return new OkResult();
            }
    }
}