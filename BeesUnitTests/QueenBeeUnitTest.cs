using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialTask_Bees;
using TrialTask_Bees.Factories;

namespace BeesUnitTests
{
    [TestClass]
    public class QueenBeeUnitTest
    {
        static QueenBee bee = null;
        int defaultId = 5;
        int defaultHealth = 20;
        int defaultHitPoints = 10;

        [TestInitialize]
        public void PrepareQueenBee()
        {
            if (bee != null) bee.Dispose();

            bee = new QueenBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestMethod]
        public void QueenBeeDefaultConstructorTest()
        {
            Bee bee = new QueenBee();
            Bee bee2 = new QueenBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestMethod]
        public void QueenBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestMethod]
        public void QueenBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Queen Bee{0}", defaultId), bee.Name);
        }

        [TestMethod]
        public void QueenBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Queen Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        //[TestMethod]
        //public void BeesFactoryQueenIdParameterTest()
        //{
        //    Bee _newBee = BeesFactory.CreateBee<QueenBee>(defaultId);

        //    Assert.IsInstanceOfType(_newBee, typeof(QueenBee));
        //    Assert.AreEqual(defaultId, _newBee.Id);
        //}

        //[TestMethod]
        //public void BeesFactoryQueenAllParameterTest()
        //{
        //    Bee _newBee = BeesFactory.CreateBee<QueenBee>(defaultId, defaultHealth, defaultHitPoints);

        //    Assert.IsInstanceOfType(_newBee, typeof(QueenBee));
        //    Assert.AreEqual(defaultId, _newBee.Id);
        //    Assert.AreEqual(defaultHealth, _newbee.Health);
        //    Assert.AreEqual(defaultHitPoints, _newbee.HitPoints);
        //}
    }
}
