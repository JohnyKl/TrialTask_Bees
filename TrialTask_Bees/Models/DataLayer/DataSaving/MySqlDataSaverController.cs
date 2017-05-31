using MySql.Data.MySqlClient;
using System;
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

                string serverName = "localhost";
                string userName = "root";
                string dbName = "trialtaskbees";
                string password = "";
                string connStr = "server=" + serverName +
                    ";user=" + userName +
                    ";database=" + dbName +
                    ";password=" + password + ";";
                try
                {
                    MySqlConnection conn = new MySqlConnection(connStr);
                    conn.Open();

                    string insert_game_info = "INSERT INTO `bees_game`(`id`, `HitCounter`, `TotalKilled`, `NumberOfAlivedBees`) VALUES " +
                        "({0},{1},{2},{3})";

                    insert_game_info = string.Format(insert_game_info, game.Id, game.HitCount, game.TotalKilled, game.AlivedBeesCount());
                    Logger.Log.Debug("SQL query: " + insert_game_info);

                    MySqlScript script = new MySqlScript(conn, insert_game_info);
                    int count = script.Execute();
                    Logger.Log.Debug("Executed " + count + " statement(s).");

                    string insert_bee_format = "INSERT INTO `bees_list`(`Id`, `game_id`, `type_id`, `Health`, `HitPoints`, `Name`) VALUES " +
                        "({0},{1},{2},{3},{4},\"{5}\")";

                    int bees_count = game.AlivedBeesCount();

                    for (int i = 0; i < bees_count; i++)
                    {
                        Bee currentBee = game.GetBeeByIndex(i);
                        if (currentBee != null)
                        {
                            int bee_type =
                                currentBee.GetType() == typeof(QueenBee) ? 1 :
                                currentBee.GetType() == typeof(WorkerBee) ? 2 :
                                currentBee.GetType() == typeof(DroneBee) ? 3 : 0;

                            string insert_bee = string.Format(insert_bee_format, currentBee.Id, game.Id, bee_type, currentBee.Health, currentBee.HitPoints, currentBee.Name);
                            Logger.Log.Debug("SQL query: " + insert_bee);

                            script = new MySqlScript(conn, insert_bee);
                            count = script.Execute();
                            Logger.Log.Debug("Executed " + count + " statement(s).");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }
    }
}