
using System;
using System.Data.SqlClient;
using System.Text;

namespace T11
{
	public class UserManager
	{
		SqlConnection conn;
		public UserManager()
		{
            DbConnector connector = new DbConnector();
            connector.Server = "172.16.2.164";
            connector.Database = "T11";
            this.conn = connector.GetConnection();
			this.Connect();
		}

        private void Connect()
        {
			this.conn.Open();
			if (this.conn.State == System.Data.ConnectionState.Open)
			{
				Console.WriteLine("Connected!");
			}
        }

		private string UpdateBuilder(string[] columns)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("UPDATE USERS SET ");
			for (int i = 0; i< columns.Length; i++)
			{
				string item = columns[i];
				sb.Append(item).Append(" = ").Append("@" + i);
				if (i < columns.Length - 1) sb.Append(",");
            }
			return sb.ToString();
		}

		public void Update(object[] values)
		{
			string[] columns = new string[] { "username", "password", "email"  };
            string sql = UpdateBuilder(columns);

            SqlCommand cmd = new SqlCommand(sql, this.conn);
			for (int i = 0; i < values.Length; i++)
			{
                cmd.Parameters.AddWithValue(i.ToString(), values[i]);
            }
            
            
        }

		public User? Login(string username, string password)
		{
			User? u = null;
			// SELECT WHERE
			string sql = "SELECT * FROM USERS WHERE username = @username AND password = @password";
            string[] args = { "hoangnm", "123123" };
			//List<Object> list = SQLUtils.Query(sql, args);
			SqlCommand cmd = new SqlCommand(sql, this.conn);
			cmd.Parameters.AddWithValue("username", username);
			cmd.Parameters.AddWithValue("password", Utils.Hash(password, "sha512"));

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				u = new User((int)reader[0], (string)reader[1], (string)reader[2]);
			}
			return u;

			//if (list.Count)
		}

        public int Register(string username, string password)
		{
			// INSERT

			string sql = "INSERT INTO USERS(username, password) VALUES(@username, @password)";

			//string[] args = { "hoangnm" , "123123" };
			//int i = SQLUtils.Execute(sql, args);


			SqlCommand cmd = new SqlCommand(sql, this.conn);
			cmd.Parameters.Add(new SqlParameter("1", "test"));
			cmd.Parameters.AddWithValue("USERS", "Product");
			cmd.Parameters.AddWithValue("username", username);
			cmd.Parameters.AddWithValue("password", Utils.Hash(password, "sha512"));


			// INSERT INTO 'Product'
			int rows = cmd.ExecuteNonQuery();
			return rows;
		}
	}
}

