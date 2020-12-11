using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlarmSystem.Functions.User
{
    public class GetUsers
    {
        private IUserService _userService;
        public GetUsers(IUserService userService){
            _userService = userService;
        }
        
        [FunctionName("GetUsers")]
       public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
            ILogger log)
            {
                List<AlarmSystem.Core.Entity.DB.User> users = _userService.GetUsers();
                
            if (users.Count == 0) {
                return new NoContentResult();
            }
            return new OkObjectResult(users);
            }
    }
}