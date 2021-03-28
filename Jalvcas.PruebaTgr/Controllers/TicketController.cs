using Microsoft.AspNetCore.Mvc;
using System;

namespace Jalvcas.PruebaTgr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet]
        public string Ticket()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
