using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketServiceAPI.Models;

namespace TicketServiceAPI.Entities
{
    public class EventTicketHeld
    {
        public int ID { get; set; }

        public int EventTicketsID { get; set; }

        public int EventID { get; set; }

        public Guid guid_Id { get; set; }

        public int HeldQuantity { get; set; }
        public int HeldAt { get; set; }

        public int HeldFor { get; set; }

        public int TotalConfirmTicket { get; set; }

        public string Session_Id { get; set; }
       
    }
}
