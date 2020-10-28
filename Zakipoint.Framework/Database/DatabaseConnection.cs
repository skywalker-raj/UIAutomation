using MySqlConnector;
using System;
using System.Data;
using Zakipoint.Framework.Driver;
namespace Zakipoint.Framework.Database
{
    public class DatabaseConnection
    {
        #region Private Members

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
                Server = Browser.Config["Server"],
                //Port = Convert.ToUInt32(Browser.Config["Port"]),
                Database = Browser.Config["Database"],
                UserID = Browser.Config["DbUiD"],
                Password = Browser.Config["DbPassword"]
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
