using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketServiceAPI.Models;

namespace TicketServiceAPI.Entities
{
	public class EventTicketsAvailablities
	{
        public int ID { get; set; }

        public int EventTicketsID { get; set; }

        public int EventID { get; set; }

        public int TotalTicketQuantity { get; set; }

        public int TotalQuantityHeld { get; set; }

        public int TotalQuantitySold { get; set; }

    }
}
