using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using TicketServiceAPI.Entities;
using TicketServiceAPI.Models;

namespace TicketServiceAPI.Services
{
	public class EventTicketAvailabilityServices
	{
        public TicketServiceContext ticketContext { get; }

        public EventTicketAvailabilityServices(TicketServiceContext context)
        {
            ticketContext = context;

        }

        public async Task<EventTicketsAvailablities> GetTotalTicketAsync(int eventticketsid, int eventid)
        {
            using var cmd = ticketContext.Connection.CreateCommand();
            cmd.CommandText = @"GetTicketAvailable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "eventticketsid",
                DbType = DbType.Int32,
                Value = eventticketsid
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "eventid",
                DbType = DbType.Int32,
                Value = eventid,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync(), eventticketsid, eventid);
            return result.Count > 0 ? result[0] : null;
        }

        private async Task<List<EventTicketsAvailablities>> ReadAllAsync(DbDataReader reader, int eventticketsid, int eventid)
        {
            var eventTicketAvailabilities = new List<EventTicketsAvailablities>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var eventTicketAvailability = new EventTicketsAvailablities()
                    {
                        ID = reader.GetInt32("id"),
                        TotalTicketQuantity = reader.GetInt32("TotalQuantity"),
                        EventTicketsID = eventticketsid,
                        EventID = eventid,
                        TotalQuantityHeld = reader.GetInt32("total_qty_held"),
                        TotalQuantitySold = reader.GetInt32("total_qty_sold")
                    };
                    eventTicketAvailabilities.Add(eventTicketAvailability);
                }
            }
            return eventTicketAvailabilities;
        }
        public async Task ConfirmTicketAsync(EventTicketHeld eventTicketHeld)
        {
            using var cmd = ticketContext.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = @"ConfirmTicket";
            BindParams(cmd, eventTicketHeld);
            await cmd.ExecuteNonQueryAsync();
            
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
                ParameterName = "confirmquantity",
                DbType = DbType.Int32,
                Value = eventTicketHeld.TotalConfirmTicket,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "sessionid",
                DbType = DbType.String,
                Value = eventTicketHeld.Session_Id,
            });

        }
    }
}
