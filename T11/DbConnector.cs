using System;
using System.Data.SqlClient;

namespace T11
{
	public class DbConnector
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public DbConnector()
        {
            Server = "172.16.3.164";
            Database = "T4";
            User = "sa";
            Password = "123123";
        }

        public DbConnector(string database, string server, string user, string password)
        {
            Database = database;
            Server = server;
            User = user;
            Password = password;
        }

        public SqlConnection GetConnection()
        {
            string connStr = BuildConnectionString();
            return new SqlConnection(connStr);
        }

        private string BuildConnectionString()
        {
            return String.Format("Data Source={0},1433;Initial Catalog={1};User Id={2};Password={3}",
                Server, Database, User, Password);
        }
    }
}
