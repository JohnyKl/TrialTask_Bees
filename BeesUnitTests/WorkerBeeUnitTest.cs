using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialTask_Bees;
using TrialTask_Bees.bees;

namespace BeesUnitTests
{
    [TestClass]
    public class WorkerBeeUnitTest
    {
        static WorkerBee bee = null;
        int defaultId = 5;
        int defaultHealth = 20;
        int defaultHitPoints = 10;

        [TestInitialize]
        public void PrepareWorkerBee()
        {
            if (bee != null) bee.Dispose();

            bee = new WorkerBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestMethod]
        public void WorkerBeeDefaultConstructorTest()
        {
            Bee bee = new WorkerBee();
            Bee bee2 = new WorkerBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestMethod]
        public void WorkerBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestMethod]
        public void WorkerBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Worker Bee{0}", defaultId), bee.Name);
        }

        [TestMethod]
        public void WorkerBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Worker Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        [TestMethod]
        public void BeesFactoryWorkerIdParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee<WorkerBee>(defaultId);

            Assert.IsInstanceOfType(_newBee, typeof(WorkerBee));
            Assert.AreEqual(defaultId, _newBee.Id);
        }

        [TestMethod]
        public void BeesFactoryWorkerAllParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee<WorkerBee>(defaultId, defaultHealth, defaultHitPoints);

            Assert.IsInstanceOfType(_newBee, typeof(WorkerBee));
            Assert.AreEqual(defaultId, _newBee.Id);
            Assert.AreEqual(defaultHealth, _newBee.Health);
            Assert.AreEqual(defaultHitPoints, _newBee.HitPoints);
        }
    }
}
