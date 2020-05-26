using MySql.Data.MySqlClient;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DataDumper.Database
{
    public class DatabaseConnection
    {
        #region Private Members

        public static readonly IConfiguration Config = new ConfigurationBuilder().AddJsonFile(@"Data/config.json").Build();

        private static readonly MySqlConnection Connection;

        private static void OpenConnection()
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Constructor

        static DatabaseConnection()
        {
            Connection = new MySqlConnection(GetConnectionString());
        }

        #endregion

        #region Public Methods

        public void CloseConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string GetConnectionString()
        {
            MySqlConnectionStringBuilder _mySqlServer = new MySqlConnectionStringBuilder
            {
                Server = Config["Server"],
                Port = Convert.ToUInt32(Config["Port"]),
                Database = Config["Database"],
                UserID = Config["DbUiD"],
                Password = Config["DbPassword"]
            };
            return _mySqlServer.ToString();
        }

        public MySqlConnection GetConnection()
        {
            OpenConnection();
            return Connection;
        }

        #endregion
    }
}