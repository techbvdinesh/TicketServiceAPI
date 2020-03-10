using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TicketServiceAPI.Entities;
using TicketServiceAPI.Models;
using TicketServiceAPI.Services;

namespace TicketServiceAPI.Controllers
{
    [Serializable]
    [Route("[controller]/[action]")]
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public TicketServiceContext ticketContext { get; }

        public TicketController(TicketServiceContext context)
        {
            ticketContext = context;
        }

        [HttpGet("{eventticketsid}/{eventid}")]
        public async Task<IActionResult> GetTotalTicket(int eventticketsid, int eventid,string sessionId)
        {
            await ticketContext.Connection.OpenAsync();
            var query = new EventTicketAvailabilityServices(ticketContext);
            var result = await query.GetTotalTicketAsync(eventticketsid, eventid);
            if (result is null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }

        [HttpPost]
       public async Task<IActionResult> PostHeldTicket([FromBody]EventTicketHeld body)
        {
            await ticketContext.Connection.OpenAsync();
            var query = new EventTicketHeldServices(ticketContext);
            await query.InsertAsync(body);
            return new OkObjectResult(body);
        }
        
        [HttpPost]
        public async Task<IActionResult> ConfirmTicket([FromBody]EventTicketHeld body)
        {
            await ticketContext.Connection.OpenAsync();
              var query = new EventTicketAvailabilityServices(ticketContext);
            await query.ConfirmTicketAsync(body);
            return new OkObjectResult(body);
        }

    }
}