using NUnit.Framework;
using TrialTask_Bees;
using TrialTask_Bees.Factories;
using TrialTask_Bees.Interfaces;

namespace BeesUnitTests
{
    [TestFixture]
    public class QueenBeeUnitTest
    {
        static QueenBee bee = null;
        static int defaultId = 5;
        static int defaultHealth = 20;
        static int defaultHitPoints = 10;
        static IGameEntityObjectInfo info = new BeeGameEntityInfo()
        {
            Number = 1,
            Type = typeof(QueenBee),
            Health = defaultHealth,
            HitPoint = defaultHitPoints
        };

        [SetUp]
        public void PrepareQueenBee()
        {
            if (bee != null) bee.Dispose();

            bee = new QueenBee(defaultId)
            {
                Health = defaultHealth,
                HitPoints = defaultHitPoints
            };
        }

        [TestCase]
        public void QueenBeeDefaultConstructorTest()
        {
            Bee bee = new QueenBee();
            Bee bee2 = new QueenBee();

            Assert.IsNotNull(bee);
            Assert.IsNotNull(bee2);
            Assert.IsTrue(bee2.Id == (bee.Id + 1));
        }

        [TestCase]
        public void QueenBeeParametersConstructorTest()
        {
            Assert.IsNotNull(bee);
            Assert.IsTrue(bee.Id == defaultId);
        }

        [TestCase]
        public void QueenBeeNamePropertyTest()
        {
            Assert.AreEqual(string.Format("Queen Bee{0}", defaultId), bee.Name);
        }

        [TestCase]
        public void QueenBeeToStringTest()
        {
            Assert.AreEqual(string.Format("Queen Bee{0} {1}", defaultId, defaultHealth), bee.ToString());
        }

        [TestCase]
        public void BeesFactoryQueenIdParameterTest()
        {
            Bee _newBee = BeesFactory.CreateBee(defaultId, info);

            Assert.IsInstanceOf<QueenBee>(_newBee);
            Assert.AreEqual(defaultId, _newBee.Id); 
        }
    }
}
