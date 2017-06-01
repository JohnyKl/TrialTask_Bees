using NUnit.Framework;
using TrialTask_Bees;
using TrialTask_Bees.Factories;
using TrialTask_Bees.Interfaces;

namespace BeesUnitTests
{
    [TestFixture]
    public class WorkerBeeUnitTest
    {
        static WorkerBee bee = null;
        static int defaultId = 5;
        static int defaultHealth = 20;
        static int defaultHitPoints = 10;
        static IGameEntityObjectInfo info = new BeeGameEntityInfo()
        {
            Number = 1,
            Type = typeof(WorkerBee),
            Health = defaultHealth,
            HitPoint = defaultHitPoints
        };

        [SetUp]
        public void PrepareWorkerBee()
        {
            if (bee != null) bee.Dispose();

            bee = new WorkerBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestCase]
        public void WorkerBeeDefaultConstructorTest()
        {
            Bee bee = new WorkerBee();
            Bee bee2 = new WorkerBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestCase]
        public void WorkerBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestCase]
        public void WorkerBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Worker Bee{0}", defaultId), bee.Name);
        }

        [TestCase]
        public void WorkerBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Worker Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        [TestCase]
        public void BeesFactoryWorkerIdParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee(defaultId, info);

            Assert.IsInstanceOf<WorkerBee>(_newBee); 
            Assert.AreEqual(defaultId, _newBee.Id);
        }
    }
}
