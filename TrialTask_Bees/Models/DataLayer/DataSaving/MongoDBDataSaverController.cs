using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.DataSaving
{
    public class MongoDBDataSaverController : IDataSaverController
    {
        static string dbName = ConfigurationManager.ConnectionStrings["mongodb_dbName"].ConnectionString;
        static string connectionString = ConfigurationManager.ConnectionStrings["mongodbConnectionStr"].ConnectionString;

        public void Save<T>(T obj)
        {
            if (obj is BeesGame)
            {
                BeesGame game = obj as BeesGame;
                
                string collectionName = "beesGame";

                var document = CreateGameDocument(game);
                
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase(dbName);
                db.CreateCollection(collectionName);

                var collection = db.GetCollection<BsonDocument>(collectionName);
                
                collection.InsertOne(document);
            }
        }

        private BsonDocument CreateGameDocument (BeesGame game)
        {
            BsonDocument document = new BsonDocument();

            document.Add("gameId", BsonValue.Create(game.Id));
            document.Add("hitCount", BsonValue.Create(game.HitCount));
            document.Add("totalKilled", BsonValue.Create(game.TotalKilled));
            document.Add("beesList", CreateBeesArray(game));

            Logging.Logger.Log.Debug(document.ToJson());

            return document;
        }
        private BsonDocument CreateBeeDocument(Bee bee)
        {
            BsonDocument doc = new BsonDocument();

            doc.Add("beeId", BsonValue.Create(bee.Id));
            doc.Add("health", BsonValue.Create(bee.Health));
            doc.Add("hitPoints", BsonValue.Create(bee.HitPoints));
            doc.Add("name", BsonValue.Create(bee.Name));

            return doc;
        }

        private BsonArray CreateBeesArray(BeesGame game)
        {
            int beesCount = game.AlivedBeesCount();

            BsonArray arr = new BsonArray(beesCount);

            for (int i = 0; i < beesCount; i++)
            {
                arr.Add(CreateBeeDocument(game.GetBeeByIndex(i)));
            }

            return arr;
        }
    }
}