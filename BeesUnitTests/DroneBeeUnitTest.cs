using NUnit.Framework;
using TrialTask_Bees;
using TrialTask_Bees.Factories;
using TrialTask_Bees.Interfaces;

namespace BeesUnitTests
{
    [TestFixture]
    public class DroneBeeUnitTest
    {
        static DroneBee bee = null;
        static int defaultId = 5;
        static int defaultHealth = 20;
        static int defaultHitPoints = 10;
        static IGameEntityObjectInfo info = new BeeGameEntityInfo() { Number = 1,        
                  Type = typeof(DroneBee),
                  Health = defaultHealth,
                  HitPoint = defaultHitPoints
            };

        [SetUp]
        public void PrepareDroneBee()
        {
            if (bee != null) bee.Dispose();
            
            bee = new DroneBee(defaultId)
            {
                Health = defaultHealth,
                    HitPoints = defaultHitPoints
            };
        }

        [TestCase]
        public void DroneBeeDefaultConstructorTest()
        {
            Bee bee = new DroneBee();
            Bee bee2 = new DroneBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestCase]
        public void DroneBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestCase]
        public void DroneBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Drone Bee{0}", defaultId), bee.Name);
        }

        [TestCase]
        public void DroneBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Drone Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        [TestCase]
        public void BeesFactoryDroneIdParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee(defaultId, info);

            Assert.IsInstanceOf<DroneBee>(_newBee);
            Assert.AreEqual(defaultId, _newBee.Id);
        }        
    }
}
