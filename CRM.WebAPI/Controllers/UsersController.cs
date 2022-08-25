using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CRM.WebAPI
{
    public class UsersController : AppController
    {

        public UsersController()
        {

        }

        
        [HttpGet("checkConnection")]
        public ActionResult CheckConnection()
        {  
            return Ok();
        }
    }
}
