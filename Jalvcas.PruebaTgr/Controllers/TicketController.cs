using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.AspNetCore3;
using System;
using Google.Apis.Auth;
using System.Threading.Tasks;
using System.Net;

namespace Jalvcas.PruebaTgr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> TicketAsync()
        {
            var headers = Request.Headers;
            var idToken = headers["Authorization"];
            //idToken = "abc";
            
            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken);    
               
            }
            catch (Exception)
            {
                return StatusCode(403);
            }

            return Ok(Guid.NewGuid().ToString());
        }
    }
}
