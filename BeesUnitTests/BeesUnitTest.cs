using TrialTask_Bees;
using NUnit.Framework;

namespace BeesUnitTests
{
    [TestFixture]
    public class BeesUnitTest
    {
        static Bee bee = null;
        int defaultId = 5;
        int defaultHealth = 20;
        int defaultHitPoints = 10;

        [SetUp]
        public void PrepareBee()
        {
            if (bee != null) bee.Dispose();

            bee = new DroneBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestCase]
        public void BeeHealthPropertySetZeroOrLessTest()
        {
            bee.Health = 0;

            Assert.AreEqual(Bee.MinimalHealth, bee.Health);
        }

        [TestCase]
        public void BeeHealthPropertySetTest()
        {
            Assert.AreEqual(defaultHealth, bee.Health);
        }

        [TestCase]
        public void BeeHitPointsPropertySetZeroOrLessTest()
        {
            bee.HitPoints = 0;

            Assert.AreEqual(Bee.MinimalHitPoints, bee.HitPoints);
        }

        [TestCase]
        public void BeeHitPointsPropertySetTest()
        {
            Assert.AreEqual(defaultHitPoints, bee.HitPoints);
        }

        [TestCase]
        public void BeeHitTest()
        {
            int healthBeforeHit = bee.Health;

            bee.Hit();

            Assert.IsTrue(bee.Health == (healthBeforeHit - defaultHitPoints));
        }

        [TestCase]
        public void BeeIsAliveTest()
        {
            int healthBeforeHit = bee.Health;

            bee.Hit();

            Assert.IsTrue(bee.IsAlive == ((healthBeforeHit - defaultHitPoints) > 0));
        }
    }
}
