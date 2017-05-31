using Ninject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;
using TrialTask_Bees.Models.Ninject;

namespace TrialTask_Bees.Controllers
{
    [RoutePrefix("api/beesgame")]
    public class BeesGameController : ApiController
    {
        public static IKernel ninjectKernel = new StandardKernel(new ManagerBindingModule());
        public static IGameService gameService = ninjectKernel.Get<IGameService>();

        private static string hitCounterLabelFormat = "Current Hits: {0}; Total killed: {1}";

        [Route("alived/{token}")]
        [HttpGet]
        public IHttpActionResult GetAlivedBeesString(string token)
        {
            IGame game = gameService.Get(token);
            if (game != null)
                return Ok(game.AlivesToString());
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

            Logger.Log.Debug(string.Format("Generated token - {0}", token));

            return token;
        }

        [Route("hit/{token}")]
        [HttpGet]
        public string Hit(string token)
        {
            IGame game = gameService.Get(token);
            if (game != null)
            {
                game.Hit();
                return string.Format(hitCounterLabelFormat, game.HitCount, game.TotalKilled);
            }
            return string.Format(hitCounterLabelFormat, 0, 0);
        }

        public bool IsGameOver(string token)
        {
            IGame game = gameService.Get(token);
            if (game != null)
                return game.IsGameOver();
            return true;
        }

        [Route("start")]
        [HttpPost]
        public void Start(HttpRequestMessage paramsList)
        {
            //List<IGameEntityObjectInfo> objectsInfo = new List<IGameEntityObjectInfo>();
            //IGame game = GameFactory.CreateGame<IGame>(objectsInfo);

            string res = paramsList.Content.ReadAsStringAsync().Result;

            string token;
            string[] splittedParams;

            if (ParseParams(res, 9, out token, out splittedParams))
            {
                try
                {
                    IGame game = gameService.Get(token);

                    List<IGameEntityObjectInfo> objectsInfo = new List<IGameEntityObjectInfo>();

                    objectsInfo.Add(CreateParameters<DroneBee>(splittedParams[0], splittedParams[1], splittedParams[2]));
                    objectsInfo.Add(CreateParameters<WorkerBee>(splittedParams[3], splittedParams[4], splittedParams[5]));
                    objectsInfo.Add(CreateParameters<QueenBee>(splittedParams[6], splittedParams[7], splittedParams[8]));

                    if (game != null)
                    {
                        game.Restart();
                    }
                    else
                    {
                        game = ninjectKernel.Get<IGame>();
                        game.GameObjectsParameters = objectsInfo;
                            //GameFactory.CreateGame<BeesGame>(objectsInfo);
                        gameService.Add(token, game);
                    }
                    game.GameObjectsParameters = objectsInfo;

                    game.Create();
                    Save(token);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }
            }
        }

        private bool ParseParams(string str, int expectedParamsCount, out string token, out string[] parameters)
        {
            token = "";
            parameters = new string[0];
            try
            {
                if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException("The parameters string is an empty");

                int indexOfParams = str.IndexOf("&paramsList");
                token = str.Substring(0, str.IndexOf("&paramsList"));
                token = token.Replace("token=", "");

                if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("The token is an empty");

                str = str.Substring(str.IndexOf("&paramsList"));

                parameters = str.Split(new string[] { "&paramsList=" }, StringSplitOptions.RemoveEmptyEntries);

                if (expectedParamsCount != parameters.Length) throw new ArgumentException("Expected params number for parsing don`t match a current size of an splitted params array.");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        private IGameEntityObjectInfo CreateParameters<T>(string tbNumber, string tbHealth, string tbHitPoints) where T : Bee
        {
            int number = int.Parse(tbNumber);
            int health = int.Parse(tbHealth);
            int hitPoints = int.Parse(tbHitPoints);

            BeeGameEntityInfo info = new BeeGameEntityInfo() { Number = number, Type = typeof(T), Health = health, HitPoint = hitPoints };

            return info;
        }

        [Route("save/{token}")]
        [HttpGet]
        public void Save(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
                gameService.Save(token, ninjectKernel.Get<IDataSaverController>());
        }
    }
}
