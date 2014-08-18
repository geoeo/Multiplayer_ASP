using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Application.Hub;
using Application.Utils.Interfaces;
using NUnit.Framework;
using Moq;

namespace Application.Tests.Hub
{
    /// <summary>
    /// Summary description for LobbyHubTest
    /// </summary>
    [TestFixture]
    public class LobbyHubTest
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Static;
        private Queue<string> WaitingPlayerQueueRef { get; set; } 

        public LobbyHubTest()
        {

            FieldInfo stackFieldInfo = typeof(LobbyHub).GetField("WaitingPlayerQueue", Flags);

            Assert.IsNotNull(stackFieldInfo);

            WaitingPlayerQueueRef = stackFieldInfo.GetValue(null) as Queue<string>;

            Assert.IsNotNull(WaitingPlayerQueueRef);


        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [SetUp]
        public void MyTestInitialize()
        {
            WaitingPlayerQueueRef.Clear();
            Assert.AreEqual(0,WaitingPlayerQueueRef.Count);
        }

        private MethodInfo GetPrivateMethodInfoFor(string methodName)
        {
            MethodInfo dynMethod = typeof (LobbyHub).GetMethod(methodName, Flags);
            Assert.IsNotNull(dynMethod);
            return dynMethod;
        }

        [Test]
        public void AddingOnePlayerToWaitingQueue()
        {
            var accessStackWithNew = GetPrivateMethodInfoFor("AccessQueueWithNew");
            const string player1 = "player1";

            Mock<IGameMappable> gameMapperMock = new Mock<IGameMappable>();

            accessStackWithNew.Invoke(null, new Object[] { player1, gameMapperMock.Object });

            Assert.AreEqual(1,WaitingPlayerQueueRef.Count);
            Assert.AreEqual(player1,WaitingPlayerQueueRef.Peek());
        }

        [Test]
        public void AddingTwoPlayersToWaitingQueue()
        {
            var accessStackWithNew = GetPrivateMethodInfoFor("AccessQueueWithNew");
            const string player1 = "player1";
            const string player2 = "player2";

            Mock<IGameMappable> gameMapperMock = new Mock<IGameMappable>();
            gameMapperMock.Setup(x => x.Add(player1, player2));

            accessStackWithNew.Invoke(null, new Object[] { player1, gameMapperMock.Object });
            accessStackWithNew.Invoke(null, new Object[] { player2, gameMapperMock.Object });

            Assert.AreEqual(0, WaitingPlayerQueueRef.Count);
            gameMapperMock.Verify(x => x.Add(player1, player2), Times.Once);
        }

        [Test]
        public void AddingThreePlayersToWaitingQueue()
        {
            var accessStackWithNew = GetPrivateMethodInfoFor("AccessQueueWithNew");
            const string player1 = "player1";
            const string player2 = "player2";
            const string player3 = "player3";

            Mock<IGameMappable> gameMapperMock = new Mock<IGameMappable>();
            gameMapperMock.Setup(x => x.Add(player1, player2));

            accessStackWithNew.Invoke(null, new Object[] { player1, gameMapperMock.Object });
            accessStackWithNew.Invoke(null, new Object[] { player2, gameMapperMock.Object });
            accessStackWithNew.Invoke(null, new Object[] { player3, gameMapperMock.Object });

            Assert.AreEqual(1, WaitingPlayerQueueRef.Count);
            Assert.AreEqual(player3, WaitingPlayerQueueRef.Peek());
            gameMapperMock.Verify(x => x.Add(player1, player2), Times.Once);
        }

    }

}
