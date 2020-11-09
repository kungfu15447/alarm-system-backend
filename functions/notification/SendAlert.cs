using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AlarmSystem.Functions.Notfification
{
    public class SendAlart 
    {
        public SendAlart() 
        {

        }

        [FunctionName("SendAlert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "notify")] HttpRequest req,
            ILogger log)
        {
            //TODO Create alarm log
            
            return new OkResult();
        }
    }
}