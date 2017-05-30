using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialTask_Bees;
using TrialTask_Bees.Models.bees;

namespace BeesUnitTests
{
    [TestClass]
    public class BeesUnitTest
    {
        static Bee bee = null;
        int defaultId = 5;
        int defaultHealth = 20;
        int defaultHitPoints = 10;

        [TestInitialize]
        public void PrepareBee()
        {
            if (bee != null) bee.Dispose();

            bee = new DroneBee(defaultId)
            {
                Parameters = new BeeParameter()
                {
                    Health = defaultHealth,
                    HitPoints = defaultHitPoints
                }               
            };
        }

        [TestMethod]
        public void BeeHealthPropertySetZeroOrLessTest()
        {
            bee.Parameters.Health = 0;
            
            Assert.AreEqual(BeeParameter.MinimalHealth, bee.Parameters.Health);
        }

        [TestMethod]
        public void BeeHealthPropertySetTest()
        {
            Assert.AreEqual(defaultHealth, bee.Parameters.Health);
        }

        [TestMethod]
        public void BeeHitPointsPropertySetZeroOrLessTest()
        {
            bee.Parameters.HitPoints = 0;

            Assert.AreEqual(BeeParameter.MinimalHitPoints, bee.Parameters.HitPoints);
        }

        [TestMethod]
        public void BeeHitPointsPropertySetTest()
        {
            Assert.AreEqual(defaultHitPoints, bee.Parameters.HitPoints);
        }

        [TestMethod]
        public void BeeHitTest()
        {
            int healthBeforeHit = bee.Parameters.Health;

            bee.Hit();

            Assert.IsTrue(bee.Parameters.Health == (healthBeforeHit - defaultHitPoints));
        }

        [TestMethod]
        public void BeeIsAliveTest()
        {
            int healthBeforeHit = bee.Parameters.Health;

            bee.Hit();

            Assert.IsTrue(bee.IsAlive == ((healthBeforeHit - defaultHitPoints) > 0));
        }
    }
}
