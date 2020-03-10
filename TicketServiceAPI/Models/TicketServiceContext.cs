using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketServiceAPI.Models
{
	public class TicketServiceContext : IDisposable
	{
        public MySqlConnection Connection { get; }

        public TicketServiceContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
