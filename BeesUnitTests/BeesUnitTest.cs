using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialTask_Bees;
using TrialTask_Bees.bees;

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
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestMethod]
        public void BeeHealthPropertySetZeroOrLessTest()
        {
            bee.Health = 0;
            
            Assert.AreEqual(Bee.MinimalHealth, bee.Health);
        }

        [TestMethod]
        public void BeeHealthPropertySetTest()
        {
            Assert.AreEqual(defaultHealth, bee.Health);
        }

        [TestMethod]
        public void BeeHitPointsPropertySetZeroOrLessTest()
        {
            bee.HitPoints = 0;

            Assert.AreEqual(Bee.MinimalHitPoints, bee.HitPoints);
        }

        [TestMethod]
        public void BeeHitPointsPropertySetTest()
        {
            Assert.AreEqual(defaultHitPoints, bee.HitPoints);
        }

        [TestMethod]
        public void BeeHitTest()
        {
            int healthBeforeHit = bee.Health;

            bee.Hit();

            Assert.IsTrue(bee.Health == (healthBeforeHit - defaultHitPoints));
        }

        [TestMethod]
        public void BeeIsAliveTest()
        {
            int healthBeforeHit = bee.Health;

            bee.Hit();

            Assert.IsTrue(bee.IsAlive == ((healthBeforeHit - defaultHitPoints) > 0));
        }
    }
}
