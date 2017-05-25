using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialTask_Bees;
using TrialTask_Bees.bees;

namespace BeesUnitTests
{
    [TestClass]
    public class DroneBeeUnitTest
    {
        static DroneBee bee = null;
        int defaultId = 5;
        int defaultHealth = 20;
        int defaultHitPoints = 10;

        [TestInitialize]
        public void PrepareDroneBee()
        {
            if (bee != null) bee.Dispose();

            bee = new DroneBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestMethod]
        public void DroneBeeDefaultConstructorTest()
        {
            Bee bee = new DroneBee();
            Bee bee2 = new DroneBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestMethod]
        public void DroneBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestMethod]
        public void DroneBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Drone Bee{0}", defaultId), bee.Name);
        }

        [TestMethod]
        public void DroneBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Drone Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        [TestMethod]
        public void BeesFactoryDroneIdParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee<DroneBee>(defaultId);

            Assert.IsInstanceOfType(_newBee, typeof(DroneBee));
            Assert.AreEqual(defaultId, _newBee.Id);
        }

        [TestMethod]
        public void BeesFactoryDroneAllParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee<DroneBee>(defaultId, defaultHealth, defaultHitPoints);

            Assert.IsInstanceOfType(_newBee, typeof(DroneBee));
            Assert.AreEqual(defaultId, _newBee.Id);
            Assert.AreEqual(defaultHealth, _newBee.Health);
            Assert.AreEqual(defaultHitPoints, _newBee.HitPoints);
        }
    }
}
