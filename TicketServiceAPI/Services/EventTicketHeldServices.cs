using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TicketServiceAPI.Entities;
using TicketServiceAPI.Models;

namespace TicketServiceAPI.Services
{
	public class EventTicketHeldServices
	{

        public TicketServiceContext ticketContext { get; }

        public EventTicketHeldServices(TicketServiceContext context)
        {
            ticketContext = context;

        }
        public async Task InsertAsync(EventTicketHeld eventTicketHeld)
        {
            using var cmd = ticketContext.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"InsertEventTicketsHeld";
            BindParams(cmd, eventTicketHeld);
            await cmd.ExecuteNonQueryAsync();
            eventTicketHeld.ID = (int)cmd.LastInsertedId;
        }
        private void BindParams(MySqlCommand cmd, EventTicketHeld eventTicketHeld)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "eventticketid",
                DbType = DbType.Int32,
                Value = eventTicketHeld.EventTicketsID,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "eventid",
                DbType = DbType.Int32,
                Value = eventTicketHeld.EventID,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "heldquantity",
                DbType = DbType.Int32,
                Value = eventTicketHeld.HeldQuantity,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "sessionid",
                DbType = DbType.String,
                Value = eventTicketHeld.Session_Id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "new_guid",
                DbType = DbType.String,
                Direction = ParameterDirection.Output,
            });
        }
    }
}
