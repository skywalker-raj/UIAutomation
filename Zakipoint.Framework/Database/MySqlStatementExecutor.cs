using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySqlConnector;

namespace Zakipoint.Framework.Database
{
    public class MySqlStatementExecutor
    {
        #region Private Fields

        private DataSet _objData;
        private static DatabaseConnection _connection;

        #endregion

        #region Constructor

        static MySqlStatementExecutor()
        {
            _connection = new DatabaseConnection();
        }

        #endregion

        #region Public Methods

        public List<string> GetTableSingleColumn(string sqlstring, int col = 0)
        {
            _objData = new DataSet();
            try
            {
                var con = _connection.GetConnection();
                var command = new MySqlCommand(sqlstring, con)
                {
                    CommandType = CommandType.Text
                };
                var dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(_objData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (_objData == null || _objData.Tables.Count == 0 || _objData.Tables[0].Rows.Count == 0)
                return null;
            return (_objData.Tables[0]).Select().Where(x => !x.IsNull(0)).AsEnumerable()
                    .Select(x => x[col].ToString()).ToList();
        }

        public long GetSingleValue(string sqlstring)
        {
            Console.WriteLine(sqlstring);
            long count = 0;
            try
            {
                var con = _connection.GetConnection();
                MySqlCommand command = new MySqlCommand(sqlstring, con)
                {
                    CommandType = CommandType.Text
                };
                count = Convert.ToInt64(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return count;
        }

        public string GetSingleStringValue(string sqlstring)
        {
            Console.WriteLine(sqlstring);
            string value = "";
            try
            {
                var con = _connection.GetConnection();
                MySqlCommand command = new MySqlCommand(sqlstring, con)
                {
                    CommandType = CommandType.Text
                };
                value = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return value;
        }

        public IEnumerable<DataRow> GetCompleteTable(string sqlstring)
        {
            Console.WriteLine(sqlstring);
            _objData = new DataSet();
            try
            {
                var con = _connection.GetConnection();
                var command = new MySqlCommand(sqlstring, con)
                {
                    CommandType = CommandType.Text
                };
                var dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(_objData, "TableObject");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (_objData != null)
                return (_objData.Tables["TableObject"]).Select().Where(x => !x.IsNull(0)).AsEnumerable().ToList();
            return null;
        }

        public void ExecuteQuery(string sqlstring)
        {
            try
            {
                var con = _connection.GetConnection();
                MySqlCommand command;
                MySqlTransaction transaction;
                command = new MySqlCommand(sqlstring, con);
                // Start a local transaction
                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                // Assign transaction object for a pending local transaction
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                try
                {

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.CloseConnection();
            }
        }

        public void ExecuteQueryAsync(string sqlstring)
        {
            Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        var con = _connection.GetConnection();
                        MySqlCommand command;
                        MySqlTransaction transaction;
                        command = new MySqlCommand(sqlstring, con);
                        // Start a local transaction
                        transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        // Assign transaction object for a pending local transaction
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        try
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine(ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
        }

        public DataTable GetDataTable(string sqlstring)
        {
            Console.WriteLine(sqlstring);
            _objData = new DataSet();
            try
            {
                var con = _connection.GetConnection();
                var command = new MySqlCommand(sqlstring, con)
                {
                    CommandType = CommandType.Text
                };
                var dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(_objData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (_objData != null)
                return _objData.Tables[0];
            else return null;
        }

        public void CloseConnection()
        {
            _connection.CloseConnection();
        }

        #endregion
    }
}
