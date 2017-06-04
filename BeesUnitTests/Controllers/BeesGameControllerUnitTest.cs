using NUnit.Framework;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TrialTask_Bees;
using TrialTask_Bees.Controllers;
using TrialTask_Bees.Interfaces;

namespace BeesUnitTests.Controllers {
    [TestFixture]
    public class BeesGameControllerUnitTest {
        static BeesGameController controller = new BeesGameController();
        static string wrongStr = "wrong+token//str";
        static string[] values = { "15", "50", "15", "8", "75", "10", "1", "100", "5" };
        static int[] valuesInt;
        static string httpStartContent;
        static int parametersCount = 9;
        string token;

        [OneTimeSetUp]
        public void InitOnStartUp() {
            token = controller.GetToken();

            httpStartContent = "token=" + token;

            foreach (string i in values) {
                httpStartContent += "&paramsList=" + i;
            }

            ConvertStringArrayToIntArray(values, out valuesInt);
        }
        
        [TestCase]
        public void GetToken_NotNullTest() {
            string token = controller.GetToken();

            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }

        [TestCase]
        public void GetToken_DifferentEachOneTest() {
            string token1 = controller.GetToken();
            string token2 = controller.GetToken();

            Assert.AreNotEqual(token1, token2);
        }

        [TestCase]
        public void GetAlivedBeesString_NullParameterTest() {
            IHttpActionResult result = controller.GetAlivedBeesString(null);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [TestCase]
        public void GetAlivedBeesString_WrongTockenTest() {
            IHttpActionResult result = controller.GetAlivedBeesString(wrongStr);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [TestCase]
        public void Hit_NullParameterTest() {
            string result = controller.Hit(null);
            string expected = string.Format(BeesGameController.hitCounterLabelFormat, 0, 0);

            Assert.AreEqual(expected, result);
        }

        [TestCase]
        public void Hit_WrongTockenTest() {
            string result = controller.Hit(wrongStr);
            string expected = string.Format(BeesGameController.hitCounterLabelFormat, 0, 0);

            Assert.AreEqual(expected, result);
        }

        [TestCase]
        public void IsGameOver_NullParameterTest() {
            bool result = controller.IsGameOver(null);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void IsGameOver_WrongTockenTest() {
            bool result = controller.IsGameOver(wrongStr);

            Assert.IsTrue(result);
        }

        [TestCase]
        public void Save_NullParameterTest() {
            IHttpActionResult result = controller.Save(null);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [TestCase]
        public void Save_NullParameterTestWrongTockenTest() {
            IHttpActionResult result = controller.Save(wrongStr);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [TestCase]
        public void ParseParams_NullParameterTest() {
            string token;
            string[] parameters;

            bool result = controller.ParseParams(null, parametersCount, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsNotNull(token);
            Assert.IsEmpty(token);
            Assert.IsTrue(parameters.Length == 0);
        }

        [TestCase]
        public void ParseParams_WrongStrParameterTest() {
            string token;
            string[] parameters;

            bool result = controller.ParseParams(wrongStr, parametersCount, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsNotNull(token);
            Assert.IsEmpty(token);
            Assert.IsTrue(parameters.Length == 0);
        }

        [TestCase]
        public void ParseParams_WrongParametersCountTest() {
            int expectedParametersCount = 0;
            string expectedToken = "sometoken";
            string token;
            string[] parameters;
            string paramStr = "token=" + expectedToken + "&paramsList=0";

            bool result = controller.ParseParams(paramStr, expectedParametersCount, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsNotNull(token);
            Assert.AreEqual(expectedToken, token);
            Assert.AreNotEqual(expectedParametersCount, parameters.Length);
        }

        [TestCase]
        public void ParseParams_EmptyTokenValueParameterTest() {
            string expectedToken = "";
            string token;
            string[] parameters;
            string paramStr = "token=" + expectedToken + "&paramsList=0";

            bool result = controller.ParseParams(paramStr, parametersCount, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsEmpty(token);
            Assert.AreEqual(expectedToken, token);
            Assert.IsTrue(parameters.Length == 0);
        }

        [TestCase]
        public void ParseParams_EmptyEntriesParametersTest() {
            int[] values = { 1, 100, 5, 8, 75, 10, 15, 50, 15 };
            string expectedToken = "sometoken";
            string token;
            string[] parameters;
            string paramStr = "token=" + expectedToken;

            for (int i = 0; i < values.Length; i++) {
                paramStr += "&paramsList=" + (i == 0 ? "" : values[i].ToString()); // creating an empty entry
            }

            bool result = controller.ParseParams(paramStr, values.Length, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsNotNull(token);
            Assert.AreEqual(expectedToken, token);
            Assert.AreNotEqual(values.Length, parameters.Length);
        }

        [TestCase]
        public void ParseParams_StrWithoutParamsTest() {
            string token;
            string[] parameters;
            string paramStr = "token=some_token";

            bool result = controller.ParseParams(paramStr, parametersCount, out token, out parameters);

            Assert.IsFalse(result);
            Assert.IsNotNull(token);
            Assert.IsEmpty(token);
            Assert.IsTrue(parameters.Length == 0);
        }

        [TestCase]
        public void ParseParams_RightParametersTest() {
            string parsedToken;
            string[] parameters;

            bool result = controller.ParseParams(httpStartContent, values.Length, out parsedToken, out parameters);

            Assert.IsTrue(result);
            Assert.IsNotNull(parsedToken);
            Assert.AreEqual(token, parsedToken);
            Assert.AreEqual(values.Length, parameters.Length);
            Assert.AreEqual(values, parameters);
        }
        
        [TestCase]
        public void Start_RightParametersTest() {
            int gamesCounterBefore = BeesGameController.gameService.games.Count;
            HttpRequestMessage message = new HttpRequestMessage();
            message.Content = new StringContent(httpStartContent);
            
            //--------------------------------------------------------------------------------
            controller.Start(message);
            //--------------------------------------------------------------------------------
            IGame game = BeesGameController.gameService.Get(token);
                       
            Assert.IsNotNull(game);                        
        }

        [TestCase]
        public void Start_RightParametersQueenCreationTest() {
            int gamesCounterBefore = BeesGameController.gameService.games.Count;
            HttpRequestMessage message = new HttpRequestMessage();
            message.Content = new StringContent(httpStartContent);

            int queenCount = 1;
            int queenHealth = valuesInt[7];
            int queenHitPoints = valuesInt[8];
            //--------------------------------------------------------------------------------
            controller.Start(message);
            //--------------------------------------------------------------------------------
            IGame game = BeesGameController.gameService.Get(token);

            Assert.IsNotNull(game);
            
            Assert.AreEqual(typeof(QueenBee), game.GameObjectsParameters[2].Type);
            Assert.AreEqual(queenCount, game.GameObjectsParameters[2].Number);
            Assert.AreEqual(queenHealth, game.GameObjectsParameters[2].Health);
            Assert.AreEqual(queenHitPoints, game.GameObjectsParameters[2].HitPoint);
        }

        [TestCase]
        public void Start_RightParametersDronesCreationTest() {
            int gamesCounterBefore = BeesGameController.gameService.games.Count;
            HttpRequestMessage message = new HttpRequestMessage();
            message.Content = new StringContent(httpStartContent);
            
            int droneCount = valuesInt[0];
            int droneHealth = valuesInt[1];
            int droneHitPoints = valuesInt[2];
            //--------------------------------------------------------------------------------
            controller.Start(message);
            //--------------------------------------------------------------------------------
            IGame game = BeesGameController.gameService.Get(token);
            
            Assert.IsNotNull(game);

            Assert.AreEqual(typeof(DroneBee), game.GameObjectsParameters[0].Type);
            Assert.AreEqual(droneCount, game.GameObjectsParameters[0].Number);
            Assert.AreEqual(droneHealth, game.GameObjectsParameters[0].Health);
            Assert.AreEqual(droneHitPoints, game.GameObjectsParameters[0].HitPoint);
        }

        [TestCase]
        public void Start_RightParametersWorkersCreationTest() {
            int gamesCounterBefore = BeesGameController.gameService.games.Count;
            HttpRequestMessage message = new HttpRequestMessage();
            message.Content = new StringContent(httpStartContent);

            int workerCount = valuesInt[3];
            int workerHealth = valuesInt[4];
            int workerHitPoints = valuesInt[5];
            //--------------------------------------------------------------------------------
            controller.Start(message);
            //--------------------------------------------------------------------------------
            IGame game = BeesGameController.gameService.Get(token);

            Assert.IsNotNull(game);

            Assert.AreEqual(typeof(WorkerBee), game.GameObjectsParameters[1].Type);
            Assert.AreEqual(workerCount, game.GameObjectsParameters[1].Number);
            Assert.AreEqual(workerHealth, game.GameObjectsParameters[1].Health);
            Assert.AreEqual(workerHitPoints, game.GameObjectsParameters[1].HitPoint);
        }
        
        [TestCase]
        public void Start_WrongParametersTest() {
            int gamesCounterBefore = BeesGameController.gameService.games.Count;
            HttpRequestMessage message = new HttpRequestMessage();
            
            controller.Start(message);

            Assert.AreEqual(gamesCounterBefore, BeesGameController.gameService.games.Count);
        }

        private void ConvertStringArrayToIntArray(string[] strArray, out int[] intArr) {
            intArr = new int[strArray.Length];
            try {
                for (int i = 0; i < strArray.Length; i++) {
                    intArr[i] = int.Parse(strArray[i]);
                }
            } catch (Exception ex) {
                //TODO: ADD LOGGING
            }
        }
    }
}
