using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;

namespace TrialTask_Bees.DataSaving
{
    public class MySqlBeesGameDataSaverController : IDataSaverController
    {
        public void Save<T>(T obj)
        {
            if (obj is BeesGame)
            {
                BeesGame game = obj as BeesGame;
                
                string serverName = ConfigurationManager.ConnectionStrings["mysql_serverName"].ConnectionString;
                string userName = ConfigurationManager.ConnectionStrings["mysql_userName"].ConnectionString; 
                string dbName = ConfigurationManager.ConnectionStrings["mysql_dbName"].ConnectionString;
                string password = ConfigurationManager.ConnectionStrings["mysql_password"].ConnectionString;

                string connStr = "server=" + serverName +
                    ";user=" + userName +
                    ";database=" + dbName +
                    ";password=" + password + ";";
                try
                {
                    MySqlConnection conn = new MySqlConnection(connStr);
                    conn.Open();

                    int bees_count = game.AlivedBeesCount();

                    if (Insert(conn, ConfigurationManager.AppSettings["insertBeesGameTableFormat"],
                        game.Id,
                        game.HitCount,
                        game.TotalKilled,
                        bees_count))
                    {
                        for (int i = 0; i < bees_count; i++)
                        {
                            Bee currentBee = game.GetBeeByIndex(i);
                            if (currentBee != null)
                            {
                                int bee_type =
                                    currentBee.GetType() == typeof(QueenBee) ? 1 :
                                    currentBee.GetType() == typeof(WorkerBee) ? 2 :
                                    currentBee.GetType() == typeof(DroneBee) ? 3 : 0;
                                if (!Insert(conn, ConfigurationManager.AppSettings["insertBeesListTableFormat"],
                                    currentBee.Id,
                                    game.Id,
                                    bee_type,
                                    currentBee.Health,
                                    currentBee.HitPoints,
                                    currentBee.Name))
                                {
                                    Logger.Log.Error("Insertion in the table not success.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }
        private bool Insert(MySqlConnection conn, string format, params object[] values)
        {
            try
            {
                string insertString = string.Format(format, values);
                Logger.Log.Debug("SQL query: " + insertString);

                MySqlScript script = new MySqlScript(conn, insertString);
                int count = script.Execute();
                Logger.Log.Debug("Executed " + count + " statement(s).");

                if (count > 0) return true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            return false;
        }
    }
}