using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrialTask_Bees.DataSaving;
using TrialTask_Bees.Repository;

namespace TrialTask_Bees.Controllers
{
    [RoutePrefix("api/beesgame")]
    public class BeesGameController : ApiController
    {
        private static Dictionary<string, BeesGame> games = new Dictionary<string, BeesGame>();
                
        [HttpGet]
        public IHttpActionResult GetAlivedBeesString(string token)
        {
            BeesGame game = GetGame(token);
            if (game != null)
                return Ok(game.AliveBeesToString());
            return NotFound();
        }

        [Route("init")]
        [HttpGet]
        public string GetToken()
        {
            Guid g = Guid.NewGuid();
            string token = Convert.ToBase64String(g.ToByteArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            token = token.Replace("/", "");
            token = token.Replace("\\", "");

            return token;
        }

        [Route("hit")]
        [HttpPost]
        public void Hit(string token)
        {
            BeesGame game = GetGame(token);
            if(game!= null)
                game.HitRandomBee();
        }

        public bool IsGameOver(string token)
        {
            BeesGame game = GetGame(token);
            if (game != null)
                return game.IsGameOver();
            return true;
        }

        [Route("start")]
        [HttpPost]
        public void Start(HttpRequestMessage paramsList)
        {
            BeesGame game = new BeesGame();
            //Console.WriteLine(value);
            string res = paramsList.Content.ReadAsStringAsync().Result;
            int indexOfParams = res.IndexOf("&paramsList");
            string token = res.Substring(0, res.IndexOf("&paramsList"));
            token = token.Replace("token=", "");

            res = res.Substring(res.IndexOf("&paramsList"));

            string[] splittedParams = res.Split(new string[] { "&paramsList=" }, StringSplitOptions.RemoveEmptyEntries);

            game.Restart();
            try
            {
                CreateBeesByTypes<DroneBee>(game, splittedParams[0], splittedParams[1], splittedParams[2]);
                CreateBeesByTypes<WorkerBee>(game, splittedParams[3], splittedParams[4], splittedParams[5]);
                CreateBeesByTypes<QueenBee>(game, splittedParams[6], splittedParams[7], splittedParams[8]);
            }
            catch(Exception ex)
            {
                //TODO: add logging
            }

            game.Bees.Reverse();

            if(games.ContainsKey(token))
            {
                games[token].Restart();
                games[token] = game;
            }
            else
            {
                games.Add(token, game);  
            }             
        }        

        private void CreateBeesByTypes<T>(BeesGame game, string tbNumber, string tbHealth, string tbHitPoints) where T : Bee
        {
            try
            {
                int number = int.Parse(tbNumber);
                int health = int.Parse(tbHealth);
                int hitPoints = int.Parse(tbHitPoints);

                game.CreateBee<T>(number, health, hitPoints);
            }
            catch (Exception ex)
            {
                //TODO: Add logging
            }
        }

        private BeesGame GetGame(string token)
        {
            if (games.ContainsKey(token))
            {
                return games[token];
            }
            return null;
        }

        [Route("save")]
        [HttpPost]
        public void Save(string token)
        {
            BeesGame game = GetGame(token);
            if (game != null)
            {
                string directoryPath = Environment.CurrentDirectory + "/savedFiles";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                game.Save(new InMemoryDataSaverController());
                game.Save(new RepositoryDataSaverController<InMemoryRepository<BeesGame>, BeesGame>());
                game.Save(new XmlFileDataSaverController() { Path = directoryPath + "/bees_game{0}.xml" });
            }
            else
            {
                //todo: add logging
            }
        }
    }
}
